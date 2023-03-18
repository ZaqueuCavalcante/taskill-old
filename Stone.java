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
int[][] maze = new int[ROWS][COLUMNS];

void setup()
{
    size(800, 900);
    
    setupInitialState();
}

void draw()
{
    background(100);
    fill(200);

    drawIndexes();
    
    text("STEP: " + step, (COLUMNS+2.50)*CELL_SIZE, CELL_SIZE*0.70);

    translate(CELL_SIZE/2, CELL_SIZE/2);
    
    for (int row = 0; row < ROWS; row++)
    {
        for (int column = 0; column < COLUMNS; column++)
        {
            if (maze[row][column] == 0) { fill(200); }
            if (maze[row][column] == 1) { fill(0,128,0); }
            if (maze[row][column] == 2) { fill(255,255,0); }

            rect(column*CELL_SIZE + CELL_SIZE/2, row*CELL_SIZE + CELL_SIZE/2, CELL_SIZE, CELL_SIZE);
            
            int neighbors = getNeighbors(row, column);
            fill(0);
            text(neighbors, column*CELL_SIZE + CELL_SIZE, row*CELL_SIZE + CELL_SIZE*1.15);
        }
    }
}

void keyPressed() {
    if (keyCode == 37 && step != 0) {
      step--;
    }
    if (keyCode == 39) {
      step++;
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
        maze[row][column] = 1;
    }
}

void setupInitialState()
{
    maze = new int[ROWS][COLUMNS];
    maze[(int)START_CELL.x][(int)START_CELL.y] = 2;
    maze[(int)END_CELL.x][(int)END_CELL.y] = 2;

    fillRow(1, new int[] { 4 });
    fillRow(2, new int[] { 2, 4, 5 });
    fillRow(3, new int[] { 1, 2, 5, 6 });
    fillRow(4, new int[] { 2, 4, 5 });
    fillRow(5, new int[] { 4 });
}

int getNeighbors(int row, int column)
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
    int[][] nextMaze = new int[ROWS][COLUMNS];
    nextMaze[(int)START_CELL.x][(int)START_CELL.y] = 2;
    nextMaze[(int)END_CELL.x][(int)END_CELL.y] = 2;
  
    for (int row = 0; row < ROWS; row++)
    {
        for (int column = 0; column < COLUMNS; column++)
        {
            int neighbors = getNeighbors(row, column);
            
            if (maze[row][column] == 0)
            {
                if (neighbors == 2 || neighbors == 3) { nextMaze[row][column] = 1; }
            }
            else if (maze[row][column] == 1)
            {
                if (neighbors == 4 || neighbors == 5 || neighbors == 6) { nextMaze[row][column] = 1; }
            }
        }
    }
    
    maze = nextMaze;
}
