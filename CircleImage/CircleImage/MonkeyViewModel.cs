using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monkeys.ViewModels
{
  public class Monkey
  {
    public string Name { get; set; }
    public string Location { get; set; }
    public string Details { get; set; }
    //URL for our monkey image!
    public string Image { get; set; }


    public string NameSort
    {
      get
      {
        if (string.IsNullOrWhiteSpace(Name) || Name.Length == 0)
          return "?";

        return Name[0].ToString().ToUpper();
      }
    }
  }

  public class MonkeysViewModel
  {
    public ObservableCollection<Monkey> Monkeys { get; set; }

    public MonkeysViewModel()
    {
      Monkeys = new ObservableCollection<Monkey>();
     

      Monkeys.Add(new Monkey
      {
        Name = "Capuchin Monkey",
        Location = "Central & South America",
        Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
        Image = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
      });

      Monkeys.Add(new Monkey
      {
        Name = "Blue Monkey",
        Location = "Central and East Africa",
        Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
        Image = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
      });


  

      Monkeys.Add(new Monkey
      {
        Name = "Spider Monkey",
        Location = "Central and South America",
        Details = "Spider monkeys of the genus Ateles are New World monkeys in the subfamily Atelinae, family Atelidae. Like other atelines, they are found in tropical forests of Central and South America, from southern Mexico to Brazil.",
        Image = "http://upload.wikimedia.org/wikipedia/commons/8/83/Spider_monkey_-Belize_Zoo-8b.jpg"
      });

      Monkeys.Add(new Monkey
      {
        Name = "Saki Monkey",
        Location = "South America",
        Details = "Sakis, or saki monkeys, are any of several New World monkeys of the genus Pithecia.[1] They are closely related to the bearded sakis of genus Chiropotes.",
        Image = "http://upload.wikimedia.org/wikipedia/commons/thumb/e/e6/WhiteFacedSaki.jpg/200px-WhiteFacedSaki.jpg"
      });

      Monkeys.Add(new Monkey
      {
        Name = "Emperor Tamarin",
        Location = "Amazon Basin",
        Details = "The emperor tamarin, is a species of tamarin allegedly named for its resemblance to the German emperor Wilhelm II. It lives in the southwest Amazon Basin, in east Peru, north Bolivia and in the west Brazilian states of Acre and Amazonas",
        Image = "http://upload.wikimedia.org/wikipedia/commons/5/55/Tamarin_portrait.JPG"
      });

      Monkeys.Add(new Monkey
      {
        Name = "Colobus Monkey",
        Location = "Africa",
        Details = "Black-and-white colobuses (or colobi) are Old World monkeys of the genus Colobus, native to Africa. They are closely related to the brown colobus monkeys of genus Piliocolobus.[1] The word 'colobus' comes from Greek κολοβός kolobós ('docked'), and is so named because in this genus, the thumb is a stump. Colobuses are herbivorous, eating leaves, fruit, flowers, and twigs. Their habitats include primary and secondary forests, riverine forests, and wooded grasslands; they are found more in higher-density logged forests than in other primary forests. Their ruminant-like digestive systems have enabled these leaf-eaters to occupy niches that are inaccessible to other primates.",
        Image = "http://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/Colubusmonkey.JPG/200px-Colubusmonkey.JPG"
      });

      Monkeys.Add(new Monkey
      {
        Name = "De Brazza's Monkey",
        Location = "Africa",
        Details = "De Brazza's monkey (Cercopithecus neglectus) is an Old World monkey endemic to the wetlands of central Africa. It is one of the most widespread African primates that live in forests.",
        Image = "http://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Cercopithecus_neglectus.jpg/220px-Cercopithecus_neglectus.jpg"
      });

      Monkeys.Add(new Monkey
      {
        Name = "Chimpanzee",
        Location = "West and Central Africa",
        Details = "Chimpanzees, sometimes colloquially chimps, are two extant hominid species of apes in the genus Pan. The Congo River divides the native habitats of the two species",
        Image = "http://upload.wikimedia.org/wikipedia/commons/thumb/6/62/Schimpanse_Zoo_Leipzig.jpg/220px-Schimpanse_Zoo_Leipzig.jpg"
      });

    }
  }
}