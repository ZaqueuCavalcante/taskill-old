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

int playerRow = 0;
int playerColumn = 0;

int step = 0;
int[][] currentMaze = new int[ROWS][COLUMNS];
int[][] nextMaze = new int[ROWS][COLUMNS];

ArrayList<String> path = new ArrayList<String>();

void setup()
{
    size(1200, 900);
    
    setupInitialState();

    nextGeneration();
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

    int directionCount = 0;
    for (String direction : path)
    {
        var x = directionCount*CELL_SIZE/2;
        var y = (ROWS+2)*CELL_SIZE + CELL_SIZE/2;
        text(direction + " - ", x, y);
        directionCount ++;
    }

    fill(255,0,0);
    circle(playerColumn*CELL_SIZE + CELL_SIZE, playerRow*CELL_SIZE + CELL_SIZE, CELL_SIZE/2);

    if ((playerRow-1) >= 0)
    {
        if (nextMaze[playerRow-1][playerColumn] != 1)
        {
            circle(playerColumn*CELL_SIZE + CELL_SIZE, playerRow*CELL_SIZE + CELL_SIZE*0.50, CELL_SIZE/4);
        }
    }
    if ((playerColumn+1) < COLUMNS)
    {
        if (nextMaze[playerRow][playerColumn+1] != 1)
        {
            circle(playerColumn*CELL_SIZE + CELL_SIZE*1.50, playerRow*CELL_SIZE + CELL_SIZE, CELL_SIZE/4);
        }
    }
    if ((playerRow+1) < ROWS)
    {
        if (nextMaze[playerRow+1][playerColumn] != 1)
        {
            circle(playerColumn*CELL_SIZE + CELL_SIZE, playerRow*CELL_SIZE + CELL_SIZE*1.50, CELL_SIZE/4);
        }
    }
    if ((playerColumn-1) >= 0)
    {
        if (nextMaze[playerRow][playerColumn-1] != 1)
        {
            circle(playerColumn*CELL_SIZE + CELL_SIZE*0.50, playerRow*CELL_SIZE + CELL_SIZE, CELL_SIZE/4);
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
}

void keyPressed() {
    if (keyCode == 37 && playerColumn > 0)
    {
      playerColumn--;
      step++;
      currentMaze = nextMaze;
      nextGeneration();
      path.add("L");
    }
    if (keyCode == 38 && playerRow > 0)
    {
      playerRow--;
      step++;
      currentMaze = nextMaze;
      nextGeneration();
      path.add("U");
    }
    if (keyCode == 39 && playerColumn < COLUMNS-1)
    {
      playerColumn++;
      step++;
      currentMaze = nextMaze;
      nextGeneration();
      path.add("R");
    }
    if (keyCode == 40 && playerRow < ROWS-1)
    {
      playerRow++;
      step++;
      currentMaze = nextMaze;
      nextGeneration();
      path.add("D");
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
