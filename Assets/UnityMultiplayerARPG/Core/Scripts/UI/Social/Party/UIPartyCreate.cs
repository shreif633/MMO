﻿using Cysharp.Threading.Tasks;
using LiteNetLibManager;
using UnityEngine.UI;

namespace MultiplayerARPG
{
    public class UIPartyCreate : UIBase
    {
        public Toggle toggleShareExp;
        public Toggle toggleShareItem;

        public void Show(bool shareExp, bool shareItem)
        {
            base.Show();
            if (toggleShareExp != null)
                toggleShareExp.isOn = shareExp;
            if (toggleShareItem != null)
                toggleShareItem.isOn = shareItem;
        }

        public void OnClickCreate()
        {
            GameInstance.ClientPartyHandlers.RequestCreateParty(new RequestCreatePartyMessage()
            {
                shareExp = toggleShareExp != null && toggleShareExp.isOn,
                shareItem = toggleShareItem != null && toggleShareItem.isOn,
            }, CreatePartyCallback);
        }

        private void CreatePartyCallback(ResponseHandlerData requestHandler, AckResponseCode responseCode, ResponseCreatePartyMessage response)
        {
            ClientPartyActions.ResponseCreateParty(requestHandler, responseCode, response);
            if (responseCode.ShowUnhandledResponseMessageDialog(response.message)) return;
            Hide();
        }
    }
}
