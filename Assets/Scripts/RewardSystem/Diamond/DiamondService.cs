using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDiamondService
{
    PlayerSaveData LoadData();
    void SaveData(PlayerSaveData data);
}


class DiamondService : IDiamondService
{
    private ISaveService saveService;
    public DiamondService(ISaveService saveService)
    {
        this.saveService = saveService;
    }
    public PlayerSaveData LoadData()
    {
        return saveService.Load();
    }
    public void SaveData(PlayerSaveData data)
    {
        saveService.Save(data);
    }
}

