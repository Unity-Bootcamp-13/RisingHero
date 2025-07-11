public interface ISaveService
{
    void Save(PlayerSaveData data);
    PlayerSaveData Load();
    void Delete();

    PlayerSaveData ReloadFromFile();
}