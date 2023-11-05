# Code Comparison to UNET

## Network Behaviour

**UNET**
```
UnityEngine.Networking.NetworkBehaviour
```
**LiteNetLibManager**
```
LiteNetLibManager.LiteNetLibBehaviour
```


## Sync Var

**UNET**
```
[SyncVar]
public int int1;
```
**LiteNetLibManager**
```
public LiteNetLibSyncField<int> int1 = new LiteNetLibSyncField<int>();
```
Or
```
[SyncField]
public int int1;
```

## Sync List
**UNET**
```
public SyncList<int> intList1 = new SyncList<int>();
```
**LiteNetLibManager**
```
public LiteNetLibSyncList<int> intList1 = new LiteNetLibSyncList<int>();
```


## Declare Command, ClientRpc, TargetRpc
**UNET**
```
[Command]
public void CmdShoot(int bulletType) {

}

[ClientRpc]
public void RpcShowDamage(int damage) {

}

[TargetRpc]
public void TargetAlert(NetworkConnection target) {
    
}
```
**LiteNetLibManager**
```
public override void OnSetup() {
    base.OnSetup();
    RegisterNetFunction<int>(Shoot);
    RegisterNetFunction<int>(ShowDamage);
    RegisterNetFunction(Alert);
}

private void Shoot(int bulletType) {

}

private void ShowDamage(int damage) {

}

private void Alert() {

}
```

Or

```
[ServerRpc]
private void Shoot(int bulletType) {

}

[AllRpc]
private void ShowDamage(int damage) {

}

[TargetRpc]
private void Alert() {

}
```

## Call Command, ClientRpc, TargetRpc
**UNET**
```
CmdShoot(bulletType);
RpcShowDamage(damage);
TargetAlert(connectionToClient);
```
**LiteNetLibManager**
```
RPC(Shoot, bulletType);
RPC(ShowDamage, damage);
RPC(Alert, ConnectionId);
```

## Autority Checking
**UNET**
```
isServer
isClient
isLocalPlayer
```
**LiteNetLibManager**
```
IsServer
IsClient
IsOwnerClient
```
