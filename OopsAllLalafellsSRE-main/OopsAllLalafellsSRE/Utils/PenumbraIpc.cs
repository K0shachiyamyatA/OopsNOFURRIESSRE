using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;
using Penumbra.Api.IpcSubscribers;
using System;

namespace OopsAllLalafellsSRE.Utils
{
    internal class PenumbraIpc(IDalamudPluginInterface pluginInterface) : IDisposable
    {
        private readonly RedrawAll redrawAll = new(pluginInterface);
        private readonly EventSubscriber<nint, Guid, nint, nint, nint> creatingCharacterBaseEvent =
            CreatingCharacterBase.Subscriber(pluginInterface, Drawer.OnCreatingCharacterBase);

        public void Dispose()
        {
            creatingCharacterBaseEvent.Dispose();
        }

        internal void RedrawAll(RedrawType setting)
        {
            try
            {
                redrawAll.Invoke(setting);
            }
            catch (Exception ex)
            {
                Plugin.OutputChatLine($"Warning: Penumbra not found. Error: {ex.Message}\n" +
                                      "Note: if you disable Penumbra before this plugin, lalafells will stay there until updated.");
            }
        }
    }
}
