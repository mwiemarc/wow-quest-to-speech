local QTS_AddonName, QTS = ...
local QTS_ColoredAddonName = '|cFFFFFF00Quest|r|cff707070-|r|cffffffffto|r|cff707070-|r|cff00ff00Speech|r'

local checkValue = 'exmQvs3sLwMVXqe78fQL'
local copyFrame = nil
local historyFrame = nil
local questLogVisible = false
local lastSelectedQuestLogTitle = ''
local history = {}
local historyIndex = 0
local EventListener = nil

local function AddHistoryString(str)
	if not historyFrame:IsVisible() then
		for i, v in ipairs(history) do -- skip if already in history
			if v == str then
				return
			end
		end

		table.insert(history, copyFrame.dataStr)

		if (#history > 20) then -- max 20 entrys
			table.remove(history, 1)
		end
	end
end

local function ShowCopyFrame(txt, parent, isEncoded)
	if txt then
		if isEncoded then
			copyFrame.dataStr = txt
		else
			-- create and set encoded datastring
			local usePlayerSex = parent == QuestLogFrame or parent == historyFrame or parent == ItemTextFrame
			local sex = usePlayerSex and UnitSex('player') or (UnitSex('target') or UnitSex('player')) -- if from questlog or history use player gender else try get target gender
			local gender = sex == 2 and 'm' or 'f' -- use female if no sex could be defined
			local npcId = UnitExists('target') and tonumber(select(6, strsplit('-', UnitGUID('target'))), 16) or 0

			copyFrame.dataStr = QTS.util:Base64Encode(string.format('<[[%s;;%s;;%s;;%s]]>', checkValue, gender, tostring(npcId), string.gsub(txt, '\\', ' '))) -- string.gsub(txt, '\\', '\n')
		end

		-- update parent
		if copyFrame:GetParent() ~= parent then
			if parent == QuestFrame then
				copyFrame:SetPoint('TOPLEFT', parent, 'TOPLEFT', 12, 37)
			elseif parent == ItemTextFrame then
				copyFrame:SetPoint('TOPLEFT', parent, 'TOPLEFT', 12, 42)
			elseif parent == QuestLogFrame then
				copyFrame:SetPoint('TOPLEFT', parent, 'TOPLEFT', 12, 42)
			elseif parent == historyFrame then
				copyFrame:SetPoint('TOPLEFT', parent, 'TOPLEFT', 0, 52)
			end
		end

		copyFrame:Show()
		copyFrame.InfoLabel:SetText(string.format('%spress CTRL+C', QTS_GlobalConfig.autoFocus and '' or 'Click textbox and '))
		copyFrame.CopyEditBox:SetText(copyFrame.dataStr)
		copyFrame.CopyEditBox:HighlightText(0)

		if QTS_GlobalConfig.autoFocus then
			copyFrame.CopyEditBox:SetFocus(true)
		end
	end
end

local function ShowHistoryFrame()
	if QuestFrame:IsVisible() then
		QuestFrame:Hide()
	elseif GossipFrame:IsVisible() then
		GossipFrame:Hide()
	elseif ItemTextFrame:IsVisible() then
		ItemTextFrame:Hide()
	elseif QuestLogFrame:IsVisible() then
		QuestLogFrame:Hide()
	end

	local decoded = string.sub(QTS.util:Base64Decode(history[historyIndex]), 4, -4) -- decode and remove open/close tag
	local txtStart = string.find(decoded, ';;[^;;]*$') + 2

	historyFrame.PageLabel:SetText(string.format('%d/%d', historyIndex, #history))
	historyFrame.TextLabel:SetText(string.sub(decoded, txtStart))
	historyFrame:Show()

	ShowCopyFrame(history[historyIndex], historyFrame, true)
end

local function HideAllFrames()
	if QuestFrame:IsVisible() then
		QuestFrame:Hide()
	elseif GossipFrame:IsVisible() then
		GossipFrame:Hide()
	elseif ItemTextFrame:IsVisible() then
		ItemTextFrame:Hide()
	elseif QuestLogFrame:IsVisible() then
		QuestLogFrame:Hide()
	end

	if (historyFrame:IsVisible()) then
		historyFrame:Hide()
	end

	copyFrame:Hide()
end

local function QuestLogWatcher()
	if QuestLogFrame:IsVisible() and not questLogVisible then
		questLogVisible = true
		EventListener(nil, 'QUEST_LOG_SHOW')
	elseif QuestLogFrame:IsVisible() and QuestLogQuestTitle:GetText() ~= lastSelectedQuestLogTitle then
		EventListener(nil, 'QUEST_LOG_SHOW')
	elseif not QuestLogFrame:IsVisible() and questLogVisible then
		questLogVisible = false
		EventListener(nil, 'QUEST_LOG_CLOSED')
	end
end

local function CreateCopyFrame()
	local OnKeyDown = function(frame, key)
		if (key == 'C' or key == 'X') and IsControlKeyDown() then
			AddHistoryString() -- add to history
		end
	end

	local OnTextChanged = function(frame, isUserInput)
		if not isUserInput then
			return
		end

		frame:SetText(frame:GetParent().dataStr)
		frame:HighlightText(0)
		frame:SetFocus(true)
	end

	local f = CreateFrame('Frame', 'QuestToSpeechCopyFrame')
	f:SetBackdrop(
		{
			bgFile = 'Interface\\Tooltip\\UI-Tooltip-Background',
			edgeFile = 'Interface\\Tooltips\\UI-Tooltip-Border',
			tile = 1,
			tileSize = 16,
			edgeSize = 16,
			insets = {left = 4, right = 4, top = 4, bottom = 4}
		}
	)
	f:SetBackdropColor(0, 0, 0, 0.5)
	f:SetBackdropBorderColor(1, 1, 1, 0.5)
	f:SetWidth(QuestFrame:GetWidth() - 76)
	f:SetHeight(100)
	f:SetPoint('TOPLEFT', QuestFrame, 'TOPLEFT', 12, 37)
	f:EnableMouse(true)
	f:SetFrameLevel(QuestFrame:GetFrameLevel() - 1)
	f:SetScript(
		'OnLoad',
		function()
			tinsert(UISpecialFrames, f)
		end
	)

	f.CopyEditBox = CreateFrame('EditBox', f:GetName() .. 'CopyEditBox', f, 'InputBoxTemplate')
	f.CopyEditBox:SetHeight(24)
	f.CopyEditBox:SetWidth(f:GetWidth() - 25) -- +button = 100
	f.CopyEditBox:SetAutoFocus(false)
	f.CopyEditBox:SetPoint('TOPLEFT', f, 'TOPLEFT', 15, -10)
	f.CopyEditBox:SetText('')
	f.CopyEditBox:EnableKeyboard(true)
	f.CopyEditBox:SetScript('OnKeyDown', OnKeyDown) -- key down listener
	f.CopyEditBox:SetScript('OnTextChanged', OnTextChanged) -- key up listener

	f.InfoLabel = f:CreateFontString(f:GetName() .. 'InfoLabel', 'ARTWORK', 'GameFontNormal')
	f.InfoLabel:SetText(string.format('%spress CTRL+C', QTS_GlobalConfig.autoFocus and 'Click textbox and ' or ''))
	f.InfoLabel:SetTextColor(1, 1, 1, 0.5)
	f.InfoLabel:SetWidth(f:GetWidth())
	f.InfoLabel:SetJustifyH('CENTER')
	f.InfoLabel:SetFont('Fonts\\FRIZQT__.TTF', 10)
	f.InfoLabel:SetPoint('TOP', f, 'TOP', 0, -38)

	f:Hide()

	copyFrame = f
end

local function CreateHistoryFrame()
	local OnClickNext = function(frame)
		if (historyIndex + 1) <= #history then
			historyIndex = historyIndex + 1
			ShowHistoryFrame()
		end
	end

	local OnClickPrev = function(frame)
		if (historyIndex - 1) > 0 then
			historyIndex = historyIndex - 1
			ShowHistoryFrame()
		end
	end

	local OnClickClose = function(frame)
		copyFrame:Hide()
		historyFrame:Hide()
	end

	local f = CreateFrame('Frame', 'QuestToSpeechHistoryFrame')
	f:SetBackdrop(
		{
			bgFile = 'Interface\\Tooltip\\UI-Tooltip-Background',
			edgeFile = 'Interface\\Tooltips\\UI-Tooltip-Border',
			tile = 1,
			tileSize = 16,
			edgeSize = 16,
			insets = {left = 4, right = 4, top = 4, bottom = 4}
		}
	)
	f:SetBackdropColor(0, 0, 0, 1)
	f:SetBackdropBorderColor(1, 1, 1, 1)
	f:SetWidth(copyFrame:GetWidth())
	f:SetHeight(300)
	f:SetPoint('LEFT', UIParent, 'LEFT', 10, 60)
	f:EnableMouse(true)
	f:SetFrameLevel(QuestFrame:GetFrameLevel())
	f:SetScript(
		'OnLoad',
		function()
			tinsert(UISpecialFrames, f)
		end
	)

	f.TextLabel = f:CreateFontString(f:GetName() .. 'TextLabel', 'ARTWORK', 'GameFontNormal')
	f.TextLabel:SetText('')
	f.TextLabel:SetTextColor(1, 1, 1, 1)
	f.TextLabel:SetWidth(f:GetWidth() - 25)
	f.TextLabel:SetJustifyH('LEFT')
	f.TextLabel:SetFont('Fonts\\FRIZQT__.TTF', 12)
	f.TextLabel:SetPoint('TOPLEFT', f, 'TOPLEFT', 15, -10)

	f.PageLabel = f:CreateFontString(f:GetName() .. 'PageLabel', 'ARTWORK', 'GameFontNormal')
	f.PageLabel:SetText('0/0')
	f.PageLabel:SetTextColor(1, 1, 1, 1)
	f.PageLabel:SetWidth(f:GetWidth() - 25)
	f.PageLabel:SetJustifyH('CENTER')
	f.PageLabel:SetFont('Fonts\\FRIZQT__.TTF', 12)
	f.PageLabel:SetPoint('BOTTOM', f, 'BOTTOM', 0, 16)

	f.CloseButton = CreateFrame('Button', f:GetName() .. 'CloseButton', f, 'UIPanelButtonTemplate')
	f.CloseButton:SetHeight(22)
	f.CloseButton:SetWidth(60)
	f.CloseButton:SetPoint('BOTTOMLEFT', f, 'BOTTOMLEFT', 10, 10)
	f.CloseButton:SetText('Close')
	f.CloseButton:SetScript('OnClick', OnClickClose) -- click listener

	f.PrevButton = CreateFrame('Button', f:GetName() .. 'PrevButton', f, 'UIPanelButtonTemplate')
	f.PrevButton:SetHeight(22)
	f.PrevButton:SetWidth(40)
	f.PrevButton:SetPoint('BOTTOMRIGHT', f, 'BOTTOMRIGHT', -60, 10)
	f.PrevButton:SetText('<')
	f.PrevButton:SetScript('OnClick', OnClickPrev) -- click listener

	f.NextButton = CreateFrame('Button', f:GetName() .. 'NextButton', f, 'UIPanelButtonTemplate')
	f.NextButton:SetHeight(22)
	f.NextButton:SetWidth(40)
	f.NextButton:SetPoint('BOTTOMRIGHT', f, 'BOTTOMRIGHT', -10, 10)
	f.NextButton:SetText('>')
	f.NextButton:SetScript('OnClick', OnClickNext) -- click listener

	f:Hide()

	historyFrame = f
end

local function InitGlobalConfig()
	QTS_GlobalConfig = QTS_GlobalConfig or {}
	QTS.util.tableUpdate(QTS_GlobalConfig, QTS.defaultConfig.GlobalConfig)
end

EventListener = function(_, event, ...)
	if event == 'ADDON_LOADED' then
		InitGlobalConfig()
		CreateCopyFrame()
		CreateHistoryFrame()

		C_Timer.NewTicker(0.1, QuestLogWatcher) -- quest log "listener"
	elseif event == 'PLAYER_LOGIN' then
		print(string.format('%s (v%s) loaded: Type /qts for more', QTS_ColoredAddonName, GetAddOnMetadata('QuestToSpeech', 'Version')))
	elseif event == 'QUEST_DETAIL' then
		if QuestInfoDescriptionText then
			if (historyFrame:IsVisible()) then
				historyFrame:Hide()
			end

			ShowCopyFrame(QuestInfoDescriptionText:GetText(), QuestFrame)
		end
	elseif event == 'QUEST_PROGRESS' then
		if QuestProgressText then
			if (historyFrame:IsVisible()) then
				historyFrame:Hide()
			end

			ShowCopyFrame(QuestProgressText:GetText(), QuestFrame)
		end
	elseif event == 'QUEST_COMPLETE' then
		if QuestInfoRewardText then
			if (historyFrame:IsVisible()) then
				historyFrame:Hide()
			end

			ShowCopyFrame(QuestInfoRewardText:GetText(), QuestFrame)
		end
	elseif event == 'GOSSIP_SHOW' then
		if GossipGreetingText then
			if (historyFrame:IsVisible()) then
				historyFrame:Hide()
			end

			ShowCopyFrame(GossipGreetingText:GetText(), QuestFrame)
		end
	elseif event == 'ITEM_TEXT_READY' then
		if ItemTextPageText then
			if (historyFrame:IsVisible()) then
				historyFrame:Hide()
			end

			ShowCopyFrame(ItemTextGetText(), ItemTextFrame)
		end
	elseif event == 'QUEST_LOG_SHOW' then -- custom event
		if (historyFrame:IsVisible()) then
			historyFrame:Hide()
		end

		if QuestLogQuestDescription and QuestLogQuestTitle then
			lastSelectedQuestLogTitle = QuestLogQuestTitle:GetText() -- save selected quest title
			ShowCopyFrame(QuestLogQuestDescription:GetText(), QuestLogFrame)
		end
	elseif event == 'QUEST_FINISHED' then
		copyFrame:Hide()
	elseif event == 'GOSSIP_CLOSED' then
		copyFrame:Hide()
	elseif event == 'ITEM_TEXT_CLOSED' then
		copyFrame:Hide()
	elseif event == 'QUEST_LOG_CLOSED' then -- custom event
		copyFrame:Hide()
	end
end

local eventFrame = CreateFrame('Frame')
eventFrame:RegisterEvent('GOSSIP_SHOW')
eventFrame:RegisterEvent('GOSSIP_CLOSED')
eventFrame:RegisterEvent('ITEM_TEXT_READY')
eventFrame:RegisterEvent('ITEM_TEXT_CLOSED')
eventFrame:RegisterEvent('QUEST_DETAIL')
eventFrame:RegisterEvent('QUEST_PROGRESS')
eventFrame:RegisterEvent('QUEST_COMPLETE')
eventFrame:RegisterEvent('QUEST_FINISHED')
eventFrame:RegisterEvent('PLAYER_LOGIN')
eventFrame:RegisterEvent('ADDON_LOADED')
eventFrame:SetScript('OnEvent', EventListener) -- frame event listener

SLASH_QUESTTOSPEECH1 = '/questtospeech'
SLASH_QUESTTOSPEECH2 = '/qts'

SlashCmdList['QUESTTOSPEECH'] = function(msg)
	msg = string.lower(msg)

	if msg == 'help' or msg == '?' or msg == '' or msg == nil then
		print(string.format('=== %s - Available Commands ===', QTS_ColoredAddonName))
		print(string.format('%s: /qts [help|?] - Show this message', QTS_ColoredAddonName))
		print(string.format('%s: /qts history - Show a history of the last 20 copied texts', QTS_ColoredAddonName))
		print(string.format('%s: /qts autofocus on|off - Autofocus copybox on show (Current setting: %s)', QTS_ColoredAddonName, QTS_GlobalConfig.autoFocus and 'On' or 'Off'))
	elseif msg == 'history' then
		if (#history > 0) then
			historyIndex = #history
			ShowHistoryFrame()
		else
			print(string.format('%s: %s', QTS_ColoredAddonName, '|cFFFF0000No copy history|r'))
		end
	elseif msg == 'autofocus on' then
		QTS_GlobalConfig.autoFocus = true
		print(string.format('%s: Autofocus set to |cff00ff00On|r', QTS_ColoredAddonName))
	elseif msg == 'autofocus off' then
		QTS_GlobalConfig.autoFocus = false
		print(string.format('%s: Autofocus set to |cff00ff00OFF|r', QTS_ColoredAddonName))
	end
end
