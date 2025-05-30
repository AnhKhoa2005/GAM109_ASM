using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>();
    public Players playerCurrent;

    private void Start()
    {
        createRegion();
    }

    public void createRegion()
    {
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "VN1"));
        listRegion.Add(new Region(2, "VN2"));
        listRegion.Add(new Region(3, "JS"));
        listRegion.Add(new Region(4, "VS"));
    }

    public string calculate_rank(int score)
    {
        // sinh viên viết tiếp code ở đây

        if (score >= 0 && score < 100)
        {
            return "Đồng";
        }
        else if (score >= 100 && score < 500)
        {
            return "Bạc";
        }
        else if (score >= 500 && score < 1000)
        {
            return "Vàng";
        }
        else if (score >= 1000)
        {
            return "Kim Cương";
        }
        return null;
    }

    public void YC1(int ID, string Name, int Score, Region IDRegion)
    {
        // sinh viên viết tiếp code ở đây
        Players player = new Players(ID, Name, Score, IDRegion);
        playerCurrent = player;
        listPlayer.Add(player);
    }
    public void YC2()
    {
        // sinh viên viết tiếp code ở đây
        listPlayer
        .ForEach(player =>
        {
            Debug.Log($"Y02. ID: {player.ID} | Name: {player.Name} | Score: {player.Score} | Region: {player.Region.Name}, Rank: {calculate_rank(player.Score)}");
        });
    }
    public void YC3()
    {
        // sinh viên viết tiếp code ở đây
        var lowerScorePlayers = listPlayer
        .Where(player => player.Score < playerCurrent.Score)
        .ToList();
        if (lowerScorePlayers.Count > 0)
        {
            lowerScorePlayers
            .ForEach(player =>
                    {
                        Debug.Log($"Y03. Score hiện tại: {playerCurrent.Score}\nID: {player.ID} | Name: {player.Name} | Score: {player.Score} | Region: {player.Region.Name}, Rank: {calculate_rank(player.Score)}");
                    });
        }
        else
        {
            Debug.Log("Y03. Không có người chơi nào có điểm số thấp hơn điểm số hiện tại.");
        }
    }
    public void YC4()
    {
        // sinh viên viết tiếp code ở đây
    }
    public void YC5()
    {
        // sinh viên viết tiếp code ở đây
    }
    public void YC6()
    {
        // sinh viên viết tiếp code ở đây
    }
    public void YC7()
    {
        // sinh viên viết tiếp code ở đây
    }
    void CalculateAndSaveAverageScoreByRegion()
    {
        // sinh viên viết tiếp code ở đây
    }

}

[SerializeField]
public class Region
{
    public int ID;
    public string Name;
    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}

[SerializeField]
public class Players
{
    // sinh viên viết tiếp code ở đây
    public int ID;
    public string Name;
    public int Score;
    public Region Region;

    public Players(int ID, string Name, int Score, Region Region)
    {
        this.ID = ID;
        this.Name = Name;
        this.Score = Score;
        this.Region = Region;
    }
}