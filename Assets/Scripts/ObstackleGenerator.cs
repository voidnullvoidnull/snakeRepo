using UnityEngine;

public class ObstackleGenerator : MonoBehaviour, IGenerator {

	public GameObject obsacklePrefab;
    public Transform rightTop;
    public Transform leftBottom;
    public int interval = 1;

    int obstackleCount;
    public int randomSize = 5;
    public int randomSeed = 5;
    public int emptyCenterSize = 0;
    public int boardersMargin = 4;

    Vector3 cameraRightTop;
    Vector3 cameraLeftBottom;

    void Awake()
    {
        cameraRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        rightTop.position = new Vector3(cameraRightTop.x, cameraRightTop.y);
        
        cameraLeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        leftBottom.position = new Vector3(cameraLeftBottom.x, cameraLeftBottom.y);
        
       
    }

    Vector3 GetRandomCoordinates()
    {
        randomSeed += System.DateTime.Now.Second;
        System.Random rnd = new System.Random(randomSeed);
        
        int x, y;

        x = rnd.Next((int)leftBottom.position.x + boardersMargin, (int)rightTop.position.x);
        y = rnd.Next((int)leftBottom.position.y + boardersMargin, (int)rightTop.position.y);

        while (Mathf.Abs((float)x % 2) != 0)
            {
                x = rnd.Next((int)leftBottom.position.x + boardersMargin, (int)rightTop.position.x);
                randomSeed++;
            }

            while (Mathf.Abs((float)y % 2) != 0)
            {
                y = rnd.Next((int)leftBottom.position.y + boardersMargin, (int)rightTop.position.y);
                randomSeed++;
            }

        Vector3 coord = new Vector3(x, y);
        return coord;
    }


    public void Generate()
    {
        int timer = System.DateTime.Now.Millisecond;
        obstackleCount = Preferences.ObstacklesCount;
        for (int i = 0; i < obstackleCount; i++)
        {
            Vector3 coord = GetRandomCoordinates();
            while (Mathf.Abs(coord.x) < emptyCenterSize & Mathf.Abs(coord.y) < emptyCenterSize)
            {
                coord = GetRandomCoordinates();
            }
            Instantiate(obsacklePrefab, coord, Quaternion.identity);

            int Size = Random.Range(0, randomSize);
            for (int j = 0; j < Size; j++)
            {
                Vector3 childCoord = coord + new Vector3(j * interval,0);
                            if(childCoord.x<rightTop.position.x-2)
                                Instantiate(obsacklePrefab, childCoord, Quaternion.identity);
                        childCoord = coord + new Vector3(-j * interval,0);
                            if (childCoord.x > leftBottom.position.x+2)
                                Instantiate(obsacklePrefab, childCoord, Quaternion.identity);
            }
        }
        Debug.Log(System.DateTime.Now.Millisecond - timer + " миллисекунд на генерацию");
    }
}
