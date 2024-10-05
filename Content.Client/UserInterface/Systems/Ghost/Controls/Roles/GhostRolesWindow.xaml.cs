using Content.Shared.Ghost.Roles;
using Robust.Client.AutoGenerated;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Utility;

// start-backmen: whitelist
using Robust.Shared.Configuration;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
// end-backmen: whitelist

namespace Content.Client.UserInterface.Systems.Ghost.Controls.Roles
{
    [GenerateTypedNameReferences]
    public sealed partial class GhostRolesWindow : DefaultWindow
    {
        public event Action<GhostRoleInfo>? OnRoleRequestButtonClicked;
        public event Action<GhostRoleInfo>? OnRoleFollow;

        public void ClearEntries()
        {
            NoRolesMessage.Visible = true;
            EntryContainer.DisposeAllChildren();
        }

        public void AddEntry(string name, string description, bool hasAccess, FormattedMessage? reason, IEnumerable<GhostRoleInfo> roles, SpriteSystem spriteSystem)
        {
            NoRolesMessage.Visible = false;

            var entry = new GhostRolesEntry(name, description, hasAccess, reason, roles, spriteSystem);
            entry.OnRoleSelected += OnRoleRequestButtonClicked;
            entry.OnRoleFollow += OnRoleFollow;
            EntryContainer.AddChild(entry);
        }

        // start-backmen: whitelist
        public void AddDenied(int denied)
        {
            if (denied == 0)
                return;

            NoRolesMessage.Visible = false;

            var message = Loc.GetString("ghost-role-whitelist-text", ("num", denied));

            if (denied == 1)
                message = Loc.GetString("ghost-role-whitelist-text-one");

            var textLabel = new RichTextLabel();
            textLabel.SetMessage(message);
            EntryContainer.AddChild(textLabel);

            var whitelistButton = new Button();
            whitelistButton.Text = Loc.GetString("ui-escap-discord");

            var uri = IoCManager.Resolve<IUriOpener>();
            var cfg = IoCManager.Resolve<IConfigurationManager>();

            whitelistButton.OnPressed += _ =>
            {
                uri.OpenUri(cfg.GetCVar(Content.Shared.CCVar.CCVars.InfoLinksDiscord));
            };

            EntryContainer.AddChild(whitelistButton);
        }
        // end-backmen: whitelist
    }
}
