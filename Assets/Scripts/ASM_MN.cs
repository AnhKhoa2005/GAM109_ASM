using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>()
    {
        new Players(1, "Nguyễn Văn A", 1500, new Region(0, "VN")),
        new Players(2, "Trần Thị B", 300, new Region(1, "VN1")),
        new Players(3, "Lê Văn C", 800, new Region(2, "VN2")),
        new Players(4, "Phạm Thị D", 1200, new Region(3, "JS")),
        new Players(5, "Nguyễn Văn E", 600, new Region(4, "VS"))
    };
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
        Debug.Log("✅🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩🚩✅");

        Players player = new Players(ID, Name, Score, IDRegion);
        playerCurrent = player;
        listPlayer.Add(player);
    }
    public void YC2()
    {
        Debug.Log("❌ Y02-----Danh sách người chơi----- ❌");
        // sinh viên viết tiếp code ở đây
        listPlayer
        .ForEach(player =>
        {
            Debug.Log($"➡️ ID: {player.ID} | Name: {player.Name} | Score: {player.Score} | Region: {player.Region.Name}, Rank: {calculate_rank(player.Score)}");
        });
    }
    public void YC3()
    {
        Debug.Log("❌ Y03----Danh sách người chơi có điểm số thấp hơn điểm số hiện tại----- ❌");
        // sinh viên viết tiếp code ở đây
        var lowerScorePlayers = listPlayer
        .Where(player => player.Score < playerCurrent.Score)
        .ToList();
        if (lowerScorePlayers.Count > 0)
        {
            lowerScorePlayers
            .ForEach(player =>
                    {
                        Debug.Log($"➡️ Score hiện tại: {playerCurrent.Score}\nID: {player.ID} | Name: {player.Name} | Score: {player.Score} | Region: {player.Region.Name}, Rank: {calculate_rank(player.Score)}");
                    });
        }
        else
        {
            Debug.Log("➡️ Không có người chơi nào có điểm số thấp hơn điểm số hiện tại.");
        }
    }
    public void YC4()
    {
        // sinh viên viết tiếp code ở đây
        Debug.Log("❌ Y04----Tìm player theo id của người chơi hiện tại, in ra thông tin----- ❌");
        var playerFound = listPlayer.FirstOrDefault(player => player.ID == playerCurrent.ID);
        if (playerFound != null)
        {
            Debug.Log($"➡️ ID: {playerFound.ID} | Name: {playerFound.Name} | Score: {playerFound.Score} | Region: {playerFound.Region.Name}, Rank: {calculate_rank(playerFound.Score)}");
        }
        else
        {
            Debug.Log("➡️ Không tìm thấy người chơi với ID hiện tại.");
        }

    }
    public void YC5()
    {
        // sinh viên viết tiếp code ở đây
        Debug.Log("❌ Y05----Xuất thông tin Players trong listPlayer theo thứ tự giảm dần----- ❌");
        listPlayer
        .OrderByDescending(player => player.Score)
        .ToList()
        .ForEach(player =>
        {
            Debug.Log($"➡️ ID: {player.ID} | Name: {player.Name} | Score: {player.Score} | Region: {player.Region.Name}, Rank: {calculate_rank(player.Score)}");
        });
    }
    public void YC6()
    {
        // sinh viên viết tiếp code ở đây
        Debug.Log("❌ Y06----Xuất 5 players có score thấp nhất theo thứ tự tăng dần----- ❌");
        listPlayer
        .OrderBy(player => player.Score)
        .Take(5)
        .ToList()
        .ForEach(player =>
        {
            Debug.Log($"➡️ ID: {player.ID} | Name: {player.Name} | Score: {player.Score} | Region: {player.Region.Name}, Rank: {calculate_rank(player.Score)}");
        });
    }
    public void YC7()
    {
        // sinh viên viết tiếp code ở đây
        Debug.Log("❌ Y07----BXH score trung bình dựa trên Region----- ❌");
        Thread BXH = new Thread(CalculateAndSaveAverageScoreByRegion);
        BXH.Start();
        BXH.Join();

    }
    void CalculateAndSaveAverageScoreByRegion()
    {
        // sinh viên viết tiếp code ở đây
        //Nhóm điểm của những người chơi có cùng Region
        var RankByRegion = listPlayer
         .GroupBy(player => player.Region.Name)
         .Select(group => new
         {
             RegionName = group.Key,
             AverageScore = group.Average(player => player.Score)
         });

        string filePath = Path.Combine(Application.dataPath, @"RankByRegion/BXHRegion.txt");
        string directoryPath = Path.GetDirectoryName(filePath);

        //Tạo thư thư mục và file nếu chưa tồn tại
        Directory.CreateDirectory(directoryPath);
        if (!File.Exists(filePath))
            File.Create(filePath).Close();

        //Ghi dữ liệu vào file
        using (StreamWriter writer = new StreamWriter(filePath, append: false))
        {
            writer.WriteLine("🚩BXH Score Trung Bình Theo Region🚩");
            foreach (var rank in RankByRegion)
            {
                writer.WriteLine($"Region: {rank.RegionName}, Average Score: {rank.AverageScore}");
            }
        }
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