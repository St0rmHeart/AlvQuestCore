using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    public static class AlvQuestStatic
    {
        static AlvQuestStatic()
        {
            CHAR_DER_PAIRS = new Dictionary<ECharacteristic, List<EDerivative>>()
            {
                {
                    ECharacteristic.Strength, new List<EDerivative>()
                    {
                        EDerivative.Value,
                        EDerivative.TerminationMult,
                        EDerivative.AddTurnChance,
                        EDerivative.Resistance,
                    }
                },

                {
                    ECharacteristic.Endurance, new List<EDerivative>()
                    {
                        EDerivative.Value,
                        EDerivative.TerminationMult,
                        EDerivative.AddTurnChance,
                        EDerivative.MaxHealth,
                        EDerivative.CurrentHealth,
                    }
                },

                {
                    ECharacteristic.Dexterity, new List<EDerivative>()
                    {
                        EDerivative.Value,
                        EDerivative.TerminationMult,
                        EDerivative.AddTurnChance,
                    }
                },

                {
                    ECharacteristic.Fire, new List<EDerivative>()
                    {
                        EDerivative.Value,
                        EDerivative.TerminationMult,
                        EDerivative.AddTurnChance,
                        EDerivative.MaxMana,
                        EDerivative.CurrentMana,
                        EDerivative.Resistance
                    }
                },

                {
                    ECharacteristic.Water, new List<EDerivative>()
                    {
                        EDerivative.Value,
                        EDerivative.TerminationMult,
                        EDerivative.AddTurnChance,
                        EDerivative.MaxMana,
                        EDerivative.CurrentMana,
                        EDerivative.Resistance
                    }
                },

                {
                    ECharacteristic.Air, new List<EDerivative>()
                    {
                        EDerivative.Value,
                        EDerivative.TerminationMult,
                        EDerivative.AddTurnChance,
                        EDerivative.MaxMana,
                        EDerivative.CurrentMana,
                        EDerivative.Resistance
                    }
                },

                {
                    ECharacteristic.Earth, new List<EDerivative>()
                    {
                        EDerivative.Value,
                        EDerivative.TerminationMult,
                        EDerivative.AddTurnChance,
                        EDerivative.MaxMana,
                        EDerivative.CurrentMana,
                        EDerivative.Resistance
                    }
                },
            };
            DERIVATIVE_SUBSCRIPTIONS = new Dictionary<ECharacteristic, Dictionary<EDerivative, List<ECharacteristic>>>()
            {
                {
                    ECharacteristic.Strength, new Dictionary<EDerivative, List<ECharacteristic>>()
                    {
                        {
                            EDerivative.TerminationMult, new List<ECharacteristic>()
                            {
                                ECharacteristic.Strength
                            }
                        },

                        {
                            EDerivative.AddTurnChance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Strength
                            }
                        },

                        {
                            EDerivative.Resistance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Strength
                            }
                        },
                    }
                },

                {
                    ECharacteristic.Endurance, new Dictionary<EDerivative, List<ECharacteristic>>()
                    {
                        {
                            EDerivative.TerminationMult, new List<ECharacteristic>()
                            {
                                ECharacteristic.Endurance
                            }
                        },

                        {
                            EDerivative.AddTurnChance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Endurance
                            }
                        },

                        {
                            EDerivative.MaxHealth, new List<ECharacteristic>()
                            {
                                ECharacteristic.Endurance
                            }
                        },

                        {
                            EDerivative.CurrentHealth, new List<ECharacteristic>()
                            {

                            }
                        },
                    }
                },

                {
                    ECharacteristic.Dexterity, new Dictionary<EDerivative, List<ECharacteristic>>()
                    {
                        {
                            EDerivative.TerminationMult, new List<ECharacteristic>()
                            {
                                ECharacteristic.Dexterity
                            }
                        },

                        {
                            EDerivative.AddTurnChance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Dexterity
                            }
                        },
                    }
                },

                {
                    ECharacteristic.Fire, new Dictionary<EDerivative, List<ECharacteristic>>()
                    {
                        {
                            EDerivative.TerminationMult, new List<ECharacteristic>()
                            {
                                ECharacteristic.Fire
                            }
                        },

                        {
                            EDerivative.AddTurnChance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Fire
                            }
                        },

                        {
                            EDerivative.MaxMana, new List<ECharacteristic>()
                            {
                                ECharacteristic.Fire
                            }
                        },

                        {
                            EDerivative.CurrentMana, new List<ECharacteristic>()
                            {

                            }
                        },

                        {
                            EDerivative.Resistance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Fire
                            }
                        },
                    }
                },

                {
                    ECharacteristic.Water, new Dictionary<EDerivative, List<ECharacteristic>>()
                    {
                        {
                            EDerivative.TerminationMult, new List<ECharacteristic>()
                            {
                                ECharacteristic.Water
                            }
                        },

                        {
                            EDerivative.AddTurnChance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Water
                            }
                        },

                        {
                            EDerivative.MaxMana, new List<ECharacteristic>()
                            {
                                ECharacteristic.Water
                            }
                        },

                        {
                            EDerivative.CurrentMana, new List<ECharacteristic>()
                            {

                            }
                        },

                        {
                            EDerivative.Resistance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Water
                            }
                        },
                    }
                },

                {
                    ECharacteristic.Earth, new Dictionary<EDerivative, List<ECharacteristic>>()
                    {
                        {
                            EDerivative.TerminationMult, new List<ECharacteristic>()
                            {
                                ECharacteristic.Earth
                            }
                        },

                        {
                            EDerivative.AddTurnChance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Earth
                            }
                        },

                        {
                            EDerivative.MaxMana, new List<ECharacteristic>()
                            {
                                ECharacteristic.Earth
                            }
                        },

                        {
                            EDerivative.CurrentMana, new List<ECharacteristic>()
                            {

                            }
                        },

                        {
                            EDerivative.Resistance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Earth
                            }
                        },
                    }
                },

                {
                    ECharacteristic.Air, new Dictionary<EDerivative, List<ECharacteristic>>()
                    {
                        {
                            EDerivative.TerminationMult, new List<ECharacteristic>()
                            {
                                ECharacteristic.Air
                            }
                        },

                        {
                            EDerivative.AddTurnChance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Air
                            }
                        },

                        {
                            EDerivative.MaxMana, new List<ECharacteristic>()
                            {
                                ECharacteristic.Air
                            }
                        },

                        {
                            EDerivative.CurrentMana, new List<ECharacteristic>()
                            {

                            }
                        },

                        {
                            EDerivative.Resistance, new List<ECharacteristic>()
                            {
                                ECharacteristic.Air
                            }
                        },
                    }
                },

            };
            /*TEMPLATE_CHARACTER = new Character.CBuilder()
               .With_Name("TEMPLATE_CHARACTER")
               .With_XP(0)
               .With_Characteristic(ECharacteristic.Strength, 0)
               .With_Characteristic(ECharacteristic.Dexterity, 0)
               .With_Characteristic(ECharacteristic.Endurance, 0)
               .With_Characteristic(ECharacteristic.Fire, 0)
               .With_Characteristic(ECharacteristic.Water, 0)
               .With_Characteristic(ECharacteristic.Air, 0)
               .With_Characteristic(ECharacteristic.Earth, 0)
               .Build();*/
        }
        public const int HASH_CONST = 16769023;
        public const int STONE_GRID_SIZE = 8;
        public const int ACCURACY_OF_CALCULATIONS = 4;
        public static readonly int[] levelBoundaries = [0, 100, 150, 250, 400, 600, 900, 1400, 2000, 2800, 3700];
        public static readonly Dictionary<ECharacteristic, Dictionary<EDerivative, List<ECharacteristic>>> DERIVATIVE_SUBSCRIPTIONS;
        public static readonly Dictionary<ECharacteristic, List<EDerivative>> CHAR_DER_PAIRS;
        public static readonly Character TEMPLATE_CHARACTER;
        public static bool IsPpossibleToEquip(BaseEquippableObject entity, Character character)
        {
            bool IsAvailable = true;
            List<ECharacteristic> characteristics = Enum.GetValues(typeof(ECharacteristic)).Cast<ECharacteristic>().Skip(2).ToList();
            foreach (ECharacteristic characteristic in characteristics)
            {
                IsAvailable &= character[characteristic] >= entity.RequirementsForUse[characteristic];
            }
            return IsAvailable;
        }
        public static bool IsPpossibleToEquip(BEO_DTO entity, Character character)
        {
            bool IsAvailable = true;
            List<ECharacteristic> characteristics = Enum.GetValues(typeof(ECharacteristic)).Cast<ECharacteristic>().Skip(2).ToList();
            foreach (ECharacteristic characteristic in characteristics)
            {
                IsAvailable &= character[characteristic] >= entity.RequirementsForUse[characteristic];
            }
            return IsAvailable;
        }
        public static double Round(this double value)
        {
            return Math.Round(value, ACCURACY_OF_CALCULATIONS);
        }
        public static int GetLinkHashCode(List<Dictionary<string, string>> data)
        {
            int hashCode = HASH_CONST;
            foreach (var dict in data.OrderBy(d => string.Join(",", d.Keys.OrderBy(k => k))))
            {
                foreach (var kvp in dict.OrderBy(x => x.Key))
                {
                    hashCode ^= kvp.Key.GetHashCode();
                    hashCode ^= kvp.Value.GetHashCode();
                }
            }
            return hashCode;
        }
        public static int GetHashCode(List<double> data)
        {
            int hashCode = HASH_CONST;
            foreach (var item in data.OrderBy(x => x))
            {
                hashCode ^= item.GetHashCode();
            }
            return hashCode;
        }
        public static class DTOConverter
        {
            public static Dictionary<string, string> ToDTOEventLink((EPlayerType target, EEvent type) link)
            {
                return new Dictionary<string, string>()
                {
                    { "EPlayerType", link.target.ToString() },
                    { "EEvent", link.type.ToString() }
                };
            }
            public static Dictionary<string, string> ToDTOImpactLink((EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value) link)
            {
                return new Dictionary<string, string>()
                {
                    { "Target", link.target.ToString() },
                    { "Characteristic", link.characteristic.ToString() },
                    { "Derivative", link.derivative.ToString() },
                    { "Variable", link.variable.ToString() },
                    { "Value", link.value.ToString() }
                };
            }
            public static List<Dictionary<string, string>> ToDTOEventLinkList(List<(EPlayerType target, EEvent type)> links)
            {
                return links.Select(ToDTOEventLink).ToList();
            }
            public static List<Dictionary<string, string>> ToDTOImpactLinkList(List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)> links)
            {
                return links.Select(ToDTOImpactLink).ToList();
            }
            public static (EPlayerType target, EEvent type) FromDTOEventLink(Dictionary<string, string> link)
            {
                // Парсим строки из словаря обратно в нужные типы
                if (link.TryGetValue("EPlayerType", out string playerTypeStr) &&
                    link.TryGetValue("EEvent", out string eventStr))
                {
                    // Преобразуем строки в соответствующие перечисления
                    if (Enum.TryParse<EPlayerType>(playerTypeStr, out var playerType) &&
                        Enum.TryParse<EEvent>(eventStr, out var eventType))
                    {
                        return (playerType, eventType);
                    }
                    else
                    {
                        throw new ArgumentException("Ошибка при парсинге строк в перечисления");
                    }
                }
                else
                {
                    throw new ArgumentException("Отсутствуют необходимые ключи в словаре");
                }
            }
            public static (EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value) FromDTOImpactLink(Dictionary<string, string> link)
            {
                // Парсим строки из словаря обратно в нужные типы
                if (link.TryGetValue("Target", out string playerTypeStr) &&
                    link.TryGetValue("Characteristic", out string characteristicStr) &&
                    link.TryGetValue("Derivative", out string derivativeStr) &&
                    link.TryGetValue("Variable", out string variableStr) &&
                    link.TryGetValue("Value", out string valueStr))
                {
                    // Преобразуем строки в соответствующие перечисления
                    if (Enum.TryParse<EPlayerType>(playerTypeStr, out var playerType) &&
                        Enum.TryParse<ECharacteristic>(characteristicStr, out var characteristic) &&
                        Enum.TryParse<EDerivative>(derivativeStr, out var derivative) &&
                        Enum.TryParse<EVariable>(variableStr, out var variable) &&
                        double.TryParse(valueStr, out var value))
                    {
                        return (playerType, characteristic, derivative, variable, value);
                    }
                    else
                    {
                        throw new ArgumentException("Ошибка при парсинге строк в перечисления");
                    }
                }
                else
                {
                    throw new ArgumentException("Отсутствуют необходимые ключи в словаре");
                }
            }
            public static List<(EPlayerType target, EEvent type)> FromDTOEventLinkList(List<Dictionary<string, string>> links)
            {
                return links.Select(FromDTOEventLink).ToList();
            }
            public static List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)> FromDTOImpactLinkList(List<Dictionary<string, string>> links)
            {
                return links.Select(FromDTOImpactLink).ToList();
            }
            
            
            
            

            public static PassiveParameterModifier.PPM_DTO ConvertPPMtoDTO(PassiveParameterModifier ppm)
            {
                return ppm.GetDTO();
            }
            public static PassiveParameterModifier ConvertDTOtoPPM(PassiveParameterModifier.PPM_DTO dto)
            {
                return new PassiveParameterModifier.PPM_Builder().InstallDTO(dto).Build();
            }
            public static TriggerParameterModifier.TPM_DTO ConvertTPMtoDTO(TriggerParameterModifier tpm)
            {
                return tpm.GetDTO();
            }
            public static TriggerParameterModifier ConvertDTOtoTPM(TriggerParameterModifier.TPM_DTO dto)
            {
                return new TriggerParameterModifier.TPM_Builder().InstallDTO(dto).Build();
            }
        }
    }
    
}