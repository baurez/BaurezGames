namespace BaurezGames.Shared.Dicolink;

public interface IDicolinkService
{
    Task<IEnumerable<DefinitionResponse>?> GetDefinitionAsync(string mot, int limite);
}