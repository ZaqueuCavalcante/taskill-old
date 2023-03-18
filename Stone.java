// Constants
int ROWS = 7;
int COLUMNS = 8;
int CELL_SIZE = 60;
PVector START_CELL = new PVector(0, 0);
PVector END_CELL = new PVector(ROWS-1, COLUMNS-1);

// Cells
// 0 -> Empty
// 1 -> Obstacle
// 2 -> Start
// 3 -> End
// 4 -> Player

int step = 0;
int[][] currentMaze = new int[ROWS][COLUMNS];
int[][] nextMaze = new int[ROWS][COLUMNS];

String[][] paths = new String[3][100];

void setup()
{
    size(1200, 900);
    
    setupInitialState();

    nextGeneration();
    
    paths[0] = new String[] { "R" };
    paths[1] = new String[] { "D" };
}

void draw()
{
    background(100);
    fill(200);

    text("STEP: " + step, (COLUMNS+2.50)*CELL_SIZE, CELL_SIZE*0.30);

    drawIndexes();
    translate(CELL_SIZE/2, CELL_SIZE/2);
    for (int row = 0; row < ROWS; row++)
    {
        for (int column = 0; column < COLUMNS; column++)
        {
            if (currentMaze[row][column] == 0) { fill(200); }
            if (currentMaze[row][column] == 1) { fill(0,128,0); }
            if (currentMaze[row][column] == 2) { fill(255,255,0); }

            rect(column*CELL_SIZE + CELL_SIZE/2, row*CELL_SIZE + CELL_SIZE/2, CELL_SIZE, CELL_SIZE);
            
            int neighbors = getNeighbors(row, column, currentMaze);
            fill(0);
            text(neighbors, column*CELL_SIZE + CELL_SIZE, row*CELL_SIZE + CELL_SIZE*1.15);
        }
    }

    translate((COLUMNS+1)*CELL_SIZE, 0);
    for (int row = 0; row < ROWS; row++)
    {
        for (int column = 0; column < COLUMNS; column++)
        {
            if (nextMaze[row][column] == 0) { fill(200); }
            if (nextMaze[row][column] == 1) { fill(0,128,0); }
            if (nextMaze[row][column] == 2) { fill(255,255,0); }

            rect(column*CELL_SIZE + CELL_SIZE/2, row*CELL_SIZE + CELL_SIZE/2, CELL_SIZE, CELL_SIZE);
            
            int neighbors = getNeighbors(row, column, nextMaze);
            fill(0);
            text(neighbors, column*CELL_SIZE + CELL_SIZE, row*CELL_SIZE + CELL_SIZE*1.15);
        }
    }


    int pathCount = 0;
    for (String[] path : paths)
    {
      int directionCount = 0;
      for (String direction : path)
      {
          var x = directionCount*CELL_SIZE/2;
          var y = (ROWS+2)*CELL_SIZE + pathCount*CELL_SIZE/2;
          text(direction + " - ", x, y);
          directionCount ++;
      }
      pathCount ++;
    }
}

void keyPressed() {
    if (keyCode == 37 && step != 0) {
      step--;
    }
    if (keyCode == 39) {
      step++;
      currentMaze = nextMaze;
      nextGeneration();
    }
}

void drawIndexes()
{
    textSize(CELL_SIZE*0.40);
    textAlign(CENTER);
    for (int row = 0; row < ROWS; row++)
    {
        text(row, CELL_SIZE/2, row*CELL_SIZE + CELL_SIZE*1.70);
    }
    for (int column = 0; column < COLUMNS; column++)
    {
        text(column, column*CELL_SIZE + CELL_SIZE*1.50, CELL_SIZE*0.70);
    }  
}

void fillRow(int row, int[] columns)
{
    for (int column : columns)
    {
        currentMaze[row][column] = 1;
    }
}

void setupInitialState()
{
    currentMaze = new int[ROWS][COLUMNS];
    currentMaze[(int)START_CELL.x][(int)START_CELL.y] = 2;
    currentMaze[(int)END_CELL.x][(int)END_CELL.y] = 2;

    fillRow(1, new int[] { 4 });
    fillRow(2, new int[] { 2, 4, 5 });
    fillRow(3, new int[] { 1, 2, 5, 6 });
    fillRow(4, new int[] { 2, 4, 5 });
    fillRow(5, new int[] { 4 });
}

int getNeighbors(int row, int column, int[][] maze)
{
    int neighbors = 0;
    
    if ((row-1) >= 0 && (column-1) >= 0)
    {
        if (maze[row-1][column-1] == 1) { neighbors++; }
    }
    if ((row-1) >= 0)
    {
        if (maze[row-1][column] == 1) { neighbors++; }
    }
    if ((row-1) >= 0 && (column+1) < COLUMNS)
    {
        if (maze[row-1][column+1] == 1) { neighbors++; }
    }
    
    if ((column-1) >= 0)
    {
        if (maze[row][column-1] == 1) { neighbors++; }
    }
    if ((column+1) < COLUMNS)
    {
        if (maze[row][column+1] == 1) { neighbors++; }
    }
    
    if ((row+1) < ROWS && (column-1) >= 0)
    {
        if (maze[row+1][column-1] == 1) { neighbors++; }
    }
    if ((row+1) < ROWS)
    {
        if (maze[row+1][column] == 1) { neighbors++; }
    }
    if ((row+1) < ROWS && (column+1) < COLUMNS)
    {
        if (maze[row+1][column+1] == 1) { neighbors++; }
    }
    
    return neighbors;
}

void nextGeneration()
{
    nextMaze = new int[ROWS][COLUMNS];
    nextMaze[(int)START_CELL.x][(int)START_CELL.y] = 2;
    nextMaze[(int)END_CELL.x][(int)END_CELL.y] = 2;
  
    for (int row = 0; row < ROWS; row++)
    {
        for (int column = 0; column < COLUMNS; column++)
        {
            int neighbors = getNeighbors(row, column, currentMaze);
            
            if (currentMaze[row][column] == 0)
            {
                if (neighbors == 2 || neighbors == 3) { nextMaze[row][column] = 1; }
            }
            else if (currentMaze[row][column] == 1)
            {
                if (neighbors == 4 || neighbors == 5 || neighbors == 6) { nextMaze[row][column] = 1; }
            }
        }
    }
}
