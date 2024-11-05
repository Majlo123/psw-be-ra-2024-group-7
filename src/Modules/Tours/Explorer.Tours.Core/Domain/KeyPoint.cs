using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain;

public class KeyPoint : Entity
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Image { get; init; }
    public float Latitude { get; init; }
    public float Longitude { get; init; }

    public KeyPoint(string name, string description, string image, float latitude, float longitude)
    {
        if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Invalid name");
        if(string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException("Invalid description");

        Name = name;
        Description = description;
        Image = image;
        Latitude = latitude;
        Longitude = longitude;
    }
    public KeyPoint(KeyPoint keyPoint)
    {
        Name = keyPoint.Name;
        Description = keyPoint.Description;
        Image = keyPoint.Image;
        Latitude = keyPoint.Latitude;
        Longitude = keyPoint.Longitude;
    }

    public bool Valid()
    {
         throw new NotImplementedException();
    }


}
