using CupkekGames.Data;
using CupkekGames.Services;

namespace CupkekGames.RPGStats
{
    /// <summary>
    /// Resolves <see cref="AttributeDefinitionSO"/> from <see cref="AttributeEffectEntry.AttributeKey"/> via <see cref="RpgStatsConstants.AttributesCatalogId"/>.
    /// </summary>
    public static class AttributeDefinitionResolver
    {
        public static AttributeDefinitionSO TryGet(string attributeKey)
        {
            if (string.IsNullOrEmpty(attributeKey))
                return null;

            IAssetCatalog<AttributeDefinitionSO> provider =
                ServiceLocator.Get<IAssetCatalog<AttributeDefinitionSO>>(RpgStatsConstants.AttributesCatalogId, silent: true);
            if (provider == null)
                return null;

            return provider.GetValue(attributeKey);
        }
    }
}
