using Cysharp.Threading.Tasks;
using LiteNetLibManager;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class LanRpgServerGachaMessageHandlers : MonoBehaviour, IServerGachaMessageHandlers
    {
        public async UniTaskVoid HandleRequestGachaInfo(
            RequestHandlerData requestHandler, EmptyMessage request,
            RequestProceedResultDelegate<ResponseGachaInfoMessage> result)
        {
            await UniTask.Yield();
            if (!GameInstance.ServerUserHandlers.TryGetPlayerCharacter(requestHandler.ConnectionId, out IPlayerCharacterData playerCharacter))
            {
                result.InvokeError(new ResponseGachaInfoMessage()
                {
                    message = UITextKeys.UI_ERROR_NOT_LOGGED_IN,
                });
                return;
            }
            result.InvokeSuccess(new ResponseGachaInfoMessage()
            {
                cash = playerCharacter.UserCash,
                gachaIds = new List<int>(GameInstance.Gachas.Keys),
            });
        }

        public async UniTaskVoid HandleRequestOpenGacha(RequestHandlerData requestHandler, RequestOpenGachaMessage request, RequestProceedResultDelegate<ResponseOpenGachaMessage> result)
        {
            await UniTask.Yield();
            if (!GameInstance.ServerUserHandlers.TryGetPlayerCharacter(requestHandler.ConnectionId, out IPlayerCharacterData playerCharacter))
            {
                result.InvokeError(new ResponseOpenGachaMessage()
                {
                    message = UITextKeys.UI_ERROR_NOT_LOGGED_IN,
                });
                return;
            }

            if (!GameInstance.Gachas.TryGetValue(request.dataId, out Gacha gacha))
            {
                result.InvokeError(new ResponseOpenGachaMessage()
                {
                    message = UITextKeys.UI_ERROR_INVALID_DATA,
                });
                return;
            }

            int price = request.openMode == GachaOpenMode.Multiple ? gacha.MultipleModeOpenPrice : gacha.SingleModeOpenPrice;
            int cash = playerCharacter.UserCash;
            if (cash < price)
            {
                result.InvokeError(new ResponseOpenGachaMessage()
                {
                    message = UITextKeys.UI_ERROR_NOT_ENOUGH_CASH,
                });
                return;
            }

            int openCount = request.openMode == GachaOpenMode.Multiple ? gacha.MultipleModeOpenCount : 1;
            List<RewardedItem> rewardItems = gacha.GetRandomedItems(openCount);
            if (playerCharacter.IncreasingItemsWillOverwhelming(rewardItems))
            {
                result.InvokeError(new ResponseOpenGachaMessage()
                {
                    message = UITextKeys.UI_ERROR_WILL_OVERWHELMING,
                });
                return;
            }
            // Decrease cash amount
            cash -= price;
            playerCharacter.UserCash = cash;
            // Increase character items
            playerCharacter.IncreaseItems(rewardItems);
            playerCharacter.FillEmptySlots();
            // Send response message
            result.InvokeSuccess(new ResponseOpenGachaMessage()
            {
                dataId = request.dataId,
                rewardItems = rewardItems,
            });
        }
    }
}
