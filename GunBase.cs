using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtilleryHelper
{

    struct TableItem
    {
        int Range;
        int VerticalUnit;
        int RangeOffset;
        int TimeOfFlight;
        int TimeOfFlightAddition;
        int WindHorizontalOffset;
        int WindVerticalOffset;
        int AirTempOffset;
        int AirPressOffset;
        int AirDensityOffset;
    }

    class ProjectileBase
    {
        // Значения для обычной траектории.
        private int MinRange = 0;
        private int MaxRange = 0;

        // Значения для навесной траектории.
        private int MinRangeArc = 0;
        private int MaxRangeArc = 0;

        // Закручивается ли снаряд при выстреле.
        private bool DerivationExist = false;
        // Возможна ли стрельба навесом или прямой дугой.
        private bool CanArc = false;

        private List<TableItem> CorrectionTable;
        private List<TableItem> ArcCorrectionTable;

        public void AddItemInTable(TableItem item)
        {
            CorrectionTable.Add(item);
        }

        public void AddItemInArcTable(TableItem item)
        {
            ArcCorrectionTable.Add(item);
        }

        public void SetMinRange(int range)
        {
            MinRange = range;
        }

        public void SetMaxRange(int range)
        {
            MaxRange = range;
        }

        public void SetMinRangeArc(int range)
        {
            MinRangeArc = range;
        }

        public void SetMaxRangeArc(int range)
        {
            MaxRangeArc = range;
        }

        public void SetDerivationExist(bool derivation)
        {
            DerivationExist = derivation;
        }

        public void SetArcExist(bool arc)
        {
            CanArc = arc;
        }

        public bool IsDerivation()
        {
            return DerivationExist;
        }

        public bool IsArc()
        {
            return CanArc;
        }

        public int GetMinRange()
        {
            return MinRange;
        }

        public int GetMaxRange()
        {
            return MaxRange;
        }
        
        public int GetMinArcRange()
        {
            return MinRangeArc;
        }

        public int GetMaxArcRange()
        {
            return MaxRangeArc;
        }
    }

    class GunBase
    {
        private string AboutInfo = "";
        // Закручивается ли снаряд при выстреле.
        // Нужен для передачи информации в класс снаряда.
        private bool DerivationExist = false;

        // Список снарядов к оружию.
        private List<ProjectileBase> projectiles;

        // Return value can be null!
        public ProjectileBase GetProjectileFromRange(int range)
        {
            if (range <= 0)
                return null;

            foreach(ProjectileBase projectile in projectiles)
            {
                if (projectile.GetMaxRange() >= range && projectile.GetMinRange() <= range)
                    return projectile;
            }

            return null;
        }
    }
}
