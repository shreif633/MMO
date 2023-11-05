﻿using LiteNetLibManager;

namespace MultiplayerARPG
{
    public partial interface IClientCharacterHandlers
    {
        bool RequestIncreaseAttributeAmount(RequestIncreaseAttributeAmountMessage data, ResponseDelegate<ResponseIncreaseAttributeAmountMessage> callback);
        bool RequestIncreaseSkillLevel(RequestIncreaseSkillLevelMessage data, ResponseDelegate<ResponseIncreaseSkillLevelMessage> callback);
        bool RequestRespawn(RequestRespawnMessage data, ResponseDelegate<ResponseRespawnMessage> callback);
        bool RequestAvailableIcons(ResponseDelegate<ResponseAvailableIconsMessage> callback);
        bool RequestAvailableFrames(ResponseDelegate<ResponseAvailableFramesMessage> callback);
        bool RequestAvailableTitles(ResponseDelegate<ResponseAvailableTitlesMessage> callback);
        bool RequestSetIcon(RequestSetIconMessage data, ResponseDelegate<ResponseSetIconMessage> callback);
        bool RequestSetFrame(RequestSetFrameMessage data, ResponseDelegate<ResponseSetFrameMessage> callback);
        bool RequestSetTitle(RequestSetTitleMessage data, ResponseDelegate<ResponseSetTitleMessage> callback);
        void SubscribePlayerCharacter(IPlayerCharacterData playerCharacter);
        void UnsubscribePlayerCharacter(IPlayerCharacterData playerCharacter);
        bool TryGetSubscribedPlayerCharacterById(string characterId, out IPlayerCharacterData playerCharacter);
        bool TryGetSubscribedPlayerCharacterByName(string characterId, out IPlayerCharacterData playerCharacter);
        void ClearSubscribedPlayerCharacters();
    }
}
