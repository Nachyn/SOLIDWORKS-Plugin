using System;

namespace Drawer3D.Model
{
    /// <summary>
    ///     Настройки программы SOLIDWORKS
    /// </summary>
    public class SolidWorksSettings
    {
        /// <summary>
        ///     Название программы в процессах ОС
        /// </summary>
        private string _name = "SLDWORKS";

        /// <summary>
        ///     GUID программы в ОС
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        ///     Название программы в процессах ОС
        /// </summary>
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