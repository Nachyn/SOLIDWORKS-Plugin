using System;
using System.Collections.Generic;
using Drawer3D.Model.Extensions;

namespace Drawer3D.Model
{
    /// <summary>
    ///     Настройки программы SOLIDWORKS
    /// </summary>
    public class SolidWorksSettings
    {
        /// <summary>
        ///     Список версий API
        /// </summary>
        private List<int> _apiNumbers;

        /// <summary>
        ///     Название программы в процессах ОС
        /// </summary>
        private string _name = "SLDWORKS";

        /// <summary>
        ///     Список версий API
        /// </summary>
        public List<int> ApiNumbers
        {
            get => _apiNumbers;
            set
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentOutOfRangeException(nameof(ApiNumbers));
                }

                _apiNumbers = value;
            }
        }

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