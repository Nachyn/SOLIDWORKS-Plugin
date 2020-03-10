using System;

namespace Drawer3D.Model
{
    public class SolidWorksSettings
    {
        private string _name = "SLDWORKS";

        public Guid Guid { get; set; }

        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Name));
                }

                _name = value;
            }
        }
    }
}