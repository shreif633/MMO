namespace MultiplayerARPG
{
    public partial class GameExtensionInstance
    {
        [DevExtMethods("Init")]
        public static void DevExtDemo_Init()
        {
            onAddCharacterStats += OnAddCharacterStats;
            onMultiplyCharacterStatsWithNumber += OnMultiplyCharacterStatsWithNumber;
            onMultiplyCharacterStats += OnMultiplyCharacterStats;
        }

        private static void OnAddCharacterStats(ref CharacterStats a, CharacterStats b)
        {
            a.testStat1 = a.testStat1 + b.testStat1;
        }

        private static void OnMultiplyCharacterStatsWithNumber(ref CharacterStats a, float b)
        {
            a.testStat1 = a.testStat1 * b;
        }

        private static void OnMultiplyCharacterStats(ref CharacterStats a, CharacterStats b)
        {
            a.testStat1 = a.testStat1 * b.testStat1;
        }
    }
}
