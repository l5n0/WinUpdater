using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public static class MaterialSkinManagerSetup
    {
        public static void InitializeMaterialSkin(MaterialForm form)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(form);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        public static MaterialButton CreateMaterialButton(string text)
        {
            return new MaterialButton
            {
                Text = text,
                AutoSize = true
            };
        }
    }
}
