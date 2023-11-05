﻿using LiteNetLibManager;

namespace MultiplayerARPG
{
    public static class ClientGachaActions
    {
        public static System.Action<ResponseHandlerData, AckResponseCode, ResponseGachaInfoMessage> onResponseGachaInfo;
        public static System.Action<ResponseHandlerData, AckResponseCode, ResponseOpenGachaMessage> onResponseOpenGacha;

        public static void ResponseGachaInfo(ResponseHandlerData requestHandler, AckResponseCode responseCode, ResponseGachaInfoMessage response)
        {
            ClientGenericActions.ClientReceiveGameMessage(response.message);
            if (onResponseGachaInfo != null)
                onResponseGachaInfo.Invoke(requestHandler, responseCode, response);
        }

        public static void ResponseOpenGacha(ResponseHandlerData requestHandler, AckResponseCode responseCode, ResponseOpenGachaMessage response)
        {
            ClientGenericActions.ClientReceiveGameMessage(response.message);
            if (onResponseOpenGacha != null)
                onResponseOpenGacha.Invoke(requestHandler, responseCode, response);
        }
    }
}
