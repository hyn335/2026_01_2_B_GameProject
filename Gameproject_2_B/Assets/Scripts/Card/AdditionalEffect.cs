using UnityEngine;
using static CardData;

    [System.Serializable]
    public class AdditionalEffect
    {
        public AdditionalEffectType effectType;      // 추가 효과 타입
        public int effectAmount;                     // 효과 수치

        public string GetDescription()
        {
            switch (effectType)
            {
                case AdditionalEffectType.DrawCard:
                    return $"카드 {effectAmount} 장 드로우";

                case AdditionalEffectType.DiscardCard:
                    return $"카드 {effectAmount} 장 버리기";

                case AdditionalEffectType.GainMana:
                    return $"마나 {effectAmount} 획득";

                case AdditionalEffectType.ReduceEnemyMana:
                    return $"적 마나 {effectAmount} 감소";

                case AdditionalEffectType.ReduceCardCost:
                    return $"다음 카드 비용 {effectAmount} 감소";

                default:
                    return "";
            }
        }
    }

