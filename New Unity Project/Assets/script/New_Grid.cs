using System.Collections;
using System.Collections.Generic;
using script;
using UnityEngine;
using UnityEngine.UI;

public class New_Grid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //碎片预置体，也可以在代码中创建新的UI-Image，然后给它添加一个凹凸属性的组件"grid"，就不用使用此预置体
    public GameObject grid;
    //切分个数（单边）
    public int number;
    //大图宽度,界面中显示的数据，非原图数据，用于设置界面中碎片的大小
    public GameObject OriginPicture;
    //主图片
    public Texture2D MainPicture;


    //生成网格
    public void CreatGrids()
    {
        if (number > 0)
        {
            
            // 150 + 75
            var pic_width = OriginPicture.GetComponent<RectTransform>().sizeDelta.x;
            float grid_size = pic_width / number + pic_width / (2 * number);
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    GameObject a;
                    a = Instantiate(grid, Vector3.zero, Quaternion.identity) as GameObject;
                    a.name = "grid" + (i + 1).ToString() + "-" + (j + 1).ToString();
                    a.transform.parent = OriginPicture.transform.parent.GetComponentInChildren<GridContainer>().transform;
                    
                    RectTransform the_Rect = a.GetComponent<RectTransform>();
                    the_Rect.anchorMin = new Vector2(0f, 1.0f);
                    the_Rect.anchorMax = new Vector2(0f, 1.0f);
                    the_Rect.pivot = new Vector2(0f, 1.0f);
                    // 生成在原位  =>   x 和 y 作1/6偏移  
                    var offsetX = grid_size / 6.0f + grid_size / 3.0f * j;
                    var offsetY = grid_size / 6.0f + grid_size / 3.0f * i;
                    the_Rect.anchoredPosition = new Vector2(grid_size * j - offsetX, -(i * grid_size) + offsetY);
                    the_Rect.sizeDelta = new Vector2(grid_size, grid_size);
                    // 添加拖拽脚本
                    a.AddComponent<DragHandler>();
                    // 保存中心吸附位
                    var center = new Vector2((the_Rect.anchoredPosition.x + grid_size) / 2.0f,
                        (the_Rect.anchoredPosition.y + grid_size) / 2.0f);
                    a.GetComponent<DragHandler>().targetPos = the_Rect.anchoredPosition;
                }
            }
            
            GenerateRandomEdges(number);
        }
    }
    
    // 随机凹凸生成示例
    void GenerateRandomEdges(int gridSize)
    {
        // 创建一个二维数组记录每个边的状态
        int[,] horizontalEdges = new int[gridSize, gridSize + 1]; // 水平边
        int[,] verticalEdges = new int[gridSize + 1, gridSize];   // 垂直边
    
        // 随机设置内部边的凹凸状态
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 1; j < gridSize; j++)
            {
                horizontalEdges[i, j] = Random.Range(0, 2) * 2 - 1; // 随机生成 -1 或 1
            }
        }
    
        for (int i = 1; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                verticalEdges[i, j] = Random.Range(0, 2) * 2 - 1; // 随机生成 -1 或 1
            }
        }
    
        // 根据边的状态设置每个碎片的凹凸属性
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject pieceObj = GameObject.Find($"grid{i+1}-{j+1}");
                if (pieceObj != null)
                {
                    grid gridComponent = pieceObj.GetComponent<grid>();
                    gridComponent.circle_up = i > 0 ? -verticalEdges[i, j] : 0;
                    gridComponent.circle_right = j < gridSize - 1 ? horizontalEdges[i, j+1] : 0;
                    gridComponent.circle_down = i < gridSize - 1 ? verticalEdges[i+1, j] : 0;
                    gridComponent.circle_left = j > 0 ? -horizontalEdges[i, j] : 0;
                }
            }
        }
    }

    //从原图对应区域逐像素点取色，复制给碎片
    public void beClip()
    {

        //循环处理每个碎片
        for (int clip_x = number; clip_x > 0; clip_x--)
        {
            for (int clip_y = 1; clip_y <= number; clip_y++)
            {
                GameObject the_grid;
                the_grid = GameObject.Find("grid" + clip_x.ToString() + "-" + clip_y.ToString());
                //切成正方形
                int w = MainPicture.width;
                // 150 + 75
                int grid_size = w / number + w / (2 * number);
                Texture2D newTexture = new Texture2D(grid_size, grid_size);
                //透明一下底色
                for (int i1 = 0; i1 < grid_size; i1++)
                {
                    for (int j1 = 0; j1 < grid_size; j1++)
                    {
                        newTexture.SetPixel(i1, j1, Color.clear);
                    }
                }
                //复制正方形图片
                //获取碎片在原图中对应区域的左下角的坐标
                Vector2 yuandian_pos_big = Vector2.zero;
                yuandian_pos_big.x = (w / number) * (clip_y - 1);
                yuandian_pos_big.y = (w / number) * (number - clip_x);

                for (int i = 0; i < w / number; i++)
                {
                    for (int j = 0; j < w / number; j++)
                    {
                        Color color = MainPicture.GetPixel(i + (int)yuandian_pos_big.x, j + (int)yuandian_pos_big.y);
                        newTexture.SetPixel(i + w / (4 * number), j + w / (4 * number), color);
                    }
                }
                //判断每个边是否有凹凸，如果有就处理
                //碎片在大图中的中心坐标
                Vector2 center_pos_big = Vector2.zero;
                center_pos_big.x = (w / number) / 2 + (w / number) * (clip_y - 1);
                center_pos_big.y = (w / number) / 2 + (w / number) * (number - clip_x);
                //小正方形中心坐标
                Vector2 center_pos_small = Vector2.zero;
                center_pos_small.x = (w / number + w / (2 * number)) / 2;
                center_pos_small.y = (w / number + w / (2 * number)) / 2;

                grid grid_aotu = the_grid.GetComponent<grid>();

                //上方凹凸处理
                if (grid_aotu.circle_up == -1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x;
                    circle_pos_small.y = center_pos_small.y + (w / (2 * number)) - w / (12 * number);
                    for (int j = (int)(center_pos_small.y + (w / (2 * number))); j > center_pos_small.y + (w / (2 * number)) - (w / (4 * number)); j--)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float x0 = circle_pos_small.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_small.y), 2)));
                        float x1 = circle_pos_small.x + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_small.y), 2)));
                        for (int i = (int)x0; i < (int)x1; i++)
                        {
                            newTexture.SetPixel(i, j, Color.clear);
                        }
                    }
                }
                else if (grid_aotu.circle_up == 1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x;
                    circle_pos_small.y = center_pos_small.y + (w / (2 * number)) + w / (12 * number);

                    //大图中圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_big = Vector2.zero;
                    circle_pos_big.x = center_pos_big.x;
                    circle_pos_big.y = center_pos_big.y + (w / (2 * number)) + w / (12 * number);
                    //添加凸出部分要从大图像素点开始循环，便于取色
                    for (int j = (int)(center_pos_big.y + (w / (2 * number))); j < center_pos_big.y + (w / (2 * number)) + (w / (4 * number)); j++)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float x0_big = circle_pos_big.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_big.y), 2)));
                        float x1_big = circle_pos_big.x + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_big.y), 2)));
                        //列出此时小正方形中对应的坐标
                        int y_small = (int)(center_pos_small.y + (w / (2 * number))) + (j - (int)(center_pos_big.y + (w / (2 * number))));
                        float x0_small = circle_pos_small.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((y_small - circle_pos_small.y), 2)));

                        for (int i = (int)x0_big; i < (int)x1_big; i++)
                        {
                            Color color = MainPicture.GetPixel(i, j);
                            int x_small = (int)x0_small + (i - (int)x0_big);

                            newTexture.SetPixel(x_small, y_small, color);
                        }
                    }
                }

                //右边凹凸处理
                if (grid_aotu.circle_right == -1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x + (w / (2 * number)) - w / (12 * number);
                    circle_pos_small.y = center_pos_small.y;
                    for (int i = (int)(center_pos_small.x + (w / (2 * number))); i > center_pos_small.x + (w / (2 * number)) - (w / (4 * number)); i--)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float y0 = circle_pos_small.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_small.x), 2)));
                        float y1 = circle_pos_small.y + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_small.x), 2)));
                        for (int j = (int)y0; j < (int)y1; j++)
                        {
                            newTexture.SetPixel(i, j, Color.clear);
                        }
                    }
                }
                else if (grid_aotu.circle_right == 1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x + (w / (2 * number)) + w / (12 * number);
                    circle_pos_small.y = center_pos_small.y;

                    //大图中圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_big = Vector2.zero;
                    circle_pos_big.x = center_pos_big.x + (w / (2 * number)) + w / (12 * number);
                    circle_pos_big.y = center_pos_big.y;
                    //添加凸出部分要从大图像素点开始循环，便于取色
                    for (int i = (int)(center_pos_big.x + (w / (2 * number))); i < center_pos_big.x + (w / (2 * number)) + (w / (4 * number)); i++)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float y0_big = circle_pos_big.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_big.x), 2)));
                        float y1_big = circle_pos_big.y + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_big.x), 2)));
                        //列出此时小正方形中对应的坐标
                        int x_small = (int)(center_pos_small.x + (w / (2 * number))) + (i - (int)(center_pos_big.x + (w / (2 * number))));
                        float y0_small = circle_pos_small.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((x_small - circle_pos_small.x), 2)));

                        for (int j = (int)y0_big; j < (int)y1_big; j++)
                        {
                            Color color = MainPicture.GetPixel(i, j);
                            int y_small = (int)y0_small + (j - (int)y0_big);

                            newTexture.SetPixel(x_small, y_small, color);
                        }
                    }
                }

                //下方凹凸处理
                if (grid_aotu.circle_down == -1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x;
                    circle_pos_small.y = center_pos_small.y - (w / (2 * number)) + w / (12 * number);
                    for (int j = (int)(center_pos_small.y - (w / (2 * number))); j < center_pos_small.y - (w / (2 * number)) + (w / (4 * number)); j++)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float x0 = circle_pos_small.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_small.y), 2)));
                        float x1 = circle_pos_small.x + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_small.y), 2)));
                        for (int i = (int)x0; i < (int)x1; i++)
                        {
                            newTexture.SetPixel(i, j, Color.clear);
                        }
                    }
                }
                else if (grid_aotu.circle_down == 1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x;
                    circle_pos_small.y = center_pos_small.y - (w / (2 * number)) - w / (12 * number);

                    //大图中圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_big = Vector2.zero;
                    circle_pos_big.x = center_pos_big.x;
                    circle_pos_big.y = center_pos_big.y - (w / (2 * number)) - w / (12 * number);
                    //添加凸出部分要从大图像素点开始循环，便于取色
                    for (int j = (int)(center_pos_big.y - (w / (2 * number))); j > center_pos_big.y - (w / (2 * number)) - (w / (4 * number)); j--)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float x0_big = circle_pos_big.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_big.y), 2)));
                        float x1_big = circle_pos_big.x + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_big.y), 2)));
                        //列出此时小正方形中对应的坐标
                        int y_small = (int)(center_pos_small.y + (w / (2 * number))) + (j - (int)(center_pos_big.y + (w / (2 * number))));
                        float x0_small = circle_pos_small.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((y_small - circle_pos_small.y), 2)));

                        for (int i = (int)x0_big; i < (int)x1_big; i++)
                        {
                            Color color = MainPicture.GetPixel(i, j);
                            int x_small = (int)x0_small + (i - (int)x0_big);

                            newTexture.SetPixel(x_small, y_small, color);
                        }
                    }
                }

                //左边凹凸处理
                if (grid_aotu.circle_left == -1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x - (w / (2 * number)) + w / (12 * number);
                    circle_pos_small.y = center_pos_small.y;
                    for (int i = (int)(center_pos_small.x - (w / (2 * number))); i < center_pos_small.x - (w / (2 * number)) + (w / (4 * number)); i++)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float y0 = circle_pos_small.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_small.x), 2)));
                        float y1 = circle_pos_small.y + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_small.x), 2)));
                        for (int j = (int)y0; j < (int)y1; j++)
                        {
                            newTexture.SetPixel(i, j, Color.clear);
                        }
                    }
                }
                else if (grid_aotu.circle_left == 1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x - (w / (2 * number)) - w / (12 * number);
                    circle_pos_small.y = center_pos_small.y;

                    //大图中圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_big = Vector2.zero;
                    circle_pos_big.x = center_pos_big.x - (w / (2 * number)) - w / (12 * number);
                    circle_pos_big.y = center_pos_big.y;
                    //添加凸出部分要从大图像素点开始循环，便于取色
                    for (int i = (int)(center_pos_big.x - (w / (2 * number))); i > center_pos_big.x - (w / (2 * number)) - (w / (4 * number)); i--)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float y0_big = circle_pos_big.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_big.x), 2)));
                        float y1_big = circle_pos_big.y + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_big.x), 2)));
                        //列出此时小正方形中对应的坐标
                        int x_small = (int)(center_pos_small.x + (w / (2 * number))) + (i - (int)(center_pos_big.x + (w / (2 * number))));
                        float y0_small = circle_pos_small.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((x_small - circle_pos_small.x), 2)));

                        for (int j = (int)y0_big; j < (int)y1_big; j++)
                        {
                            Color color = MainPicture.GetPixel(i, j);
                            int y_small = (int)y0_small + (j - (int)y0_big);

                            newTexture.SetPixel(x_small, y_small, color);
                        }
                    }
                }

                newTexture.Apply();

                //把纹理交给碎片
                Sprite sp = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.zero);
                Image the_image = the_grid.GetComponent<Image>();
                the_image.sprite = sp;
            }
        }
    }
}
