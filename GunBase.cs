using ArtilleryHelper;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ArtilleryHelper
{

    // м,^C, м/с - метрические единицы (далее м.)
    // ед - единицы отмеченные на прицеле
    // в случае с ед. - считаем суммой, в случае
    // с м. - считаем как изменение расстояния
    // Ex.: RangePerUnit - показывает изменение
    //      расстояния полета снаряда на единицу
    //      измерения прицела.

    public struct TableItem
    {
                                                
        public double Range;                           // Дальность (м)
        public double[] ScopeValue;                    // Прицел (ед)
        public double RangePerUnit;                    // Изменение дальности на 1 единицу (м)
        public double RangeOffset;                     // Изменение прицела на 50м дальности (ед)
        public double HeightOffset;                    // Изменение прицела на 100м высоты (ед)
        public double Derivation;                      // Деривация (сдвиг горизонтальной наводки) (ед)
        public double SideWind;                        // Боковой ветер (сдвиг горизонтальной наводки) (м/с)
        public double LongitudinalWind;                // Продольный ветер (м/с)
        public double AirTemperature;                  // Температура воздуха на 10^C (15^)
        public double AirPress;                        // Давление возд. на 10hPa (1013,25)
        public double AirDensityDown;                  // Плотность воздуха на 1% (ниже) (1,221кг/м3)
        public double AirDensityUp;                    // Плотность воздуха на 1% (выше) (1,221кг/м3)
        public double TimeOfFlight;                    // Время полета (с)
        public double SideProbability;                 // Вероятность боковая (м)
        public double RangeProbability;                // Вероятность на дальность (м)

        public TableItem(double range)
        {
            Range = range;
            ScopeValue = new double[7] {0, 0, 0, 0, 0, 0, 0};
            RangePerUnit = 0;
            RangeOffset = 0;
            HeightOffset = 0;
            Derivation = 0;
            SideWind = 0;
            LongitudinalWind = 0;
            AirTemperature = 0;
            AirPress = 0;
            AirDensityDown = 0;
            AirDensityUp = 0;
            TimeOfFlight = 0;
            SideProbability = 0;
            RangeProbability = 0;
        }
    }

    public class ProjectileBase
    {
        // Значения для обычной траектории.
        public double MinRange = 10000;
        public double MaxRange = 0;

        // Значения для навесной траектории.
        public double MinRangeArc = 10000;
        public double MaxRangeArc = 0;

        // Закручивается ли снаряд при выстреле.
        public bool DerivationExist = false;
        // Возможна ли стрельба навесом или прямой дугой.
        public bool CanArc = false;

        public string Name { get; set; }

        public List<TableItem> CorrectionTable;
        public List<TableItem> ArcCorrectionTable;

        public ProjectileBase()
        {
            CorrectionTable = new List<TableItem>();
            ArcCorrectionTable = new List<TableItem>();
        }

        public void AddItemInTable(TableItem item)
        {
            CorrectionTable.Add(item);
        }

        public void AddItemInArcTable(TableItem item)
        {
            ArcCorrectionTable.Add(item);
        }

        public void SetMinRange(double range)
        {
            MinRange = range;
        }

        public void SetMaxRange(double range)
        {
            MaxRange = range;
        }

        public void SetMinRangeArc(double range)
        {
            MinRangeArc = range;
        }

        public void SetMaxRangeArc(double range)
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

        public double GetMinRange()
        {
            return MinRange;
        }

        public double GetMaxRange()
        {
            return MaxRange;
        }
        
        public double GetMinArcRange()
        {
            return MinRangeArc;
        }

        public double GetMaxArcRange()
        {
            return MaxRangeArc;
        }
    }

    public class GunBase
    {
        public string Name {get; set; }
        public string AboutInfo { get; set; }
        public bool ScaleType { get; set; }
        // Закручивается ли снаряд при выстреле.
        // Нужен для передачи информации в класс снаряда.
        public bool DerivationExist = false;

        // Список снарядов к оружию.
        public List<ProjectileBase> projectiles;

        public GunBase()
        {
            projectiles = new List<ProjectileBase>();
        }

        // Return value can be null!
        public ProjectileBase GetProjectileFromRange(double range)
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


    // Пусть будет только один
    class GunList
    {

        private static Dictionary<string, GunBase> guns;

        private GunList() { }


        public static Dictionary<string, GunBase> GetInstance()
        {
            if(guns == null)
                guns = new Dictionary<string, GunBase>();
            return guns;
        }

        public static GunBase GetGun(string name)
        {
            return GetInstance().ContainsKey(name) ? GetInstance()[name] : null;
        }
    }

}
