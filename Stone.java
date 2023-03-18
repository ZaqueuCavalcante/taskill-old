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
}

void draw()
{
    background(100);
    fill(200);

    drawIndexes();
    
    text("STEP: " + step, (COLUMNS+2.50)*CELL_SIZE, CELL_SIZE*0.70);

    translate(CELL_SIZE/2, CELL_SIZE/2);

    maze = new int[ROWS][COLUMNS];
    maze[(int)START_CELL.x][(int)START_CELL.y] = 2;
    maze[(int)END_CELL.x][(int)END_CELL.y] = 2;

    if (step == 0) { setupStep00(); }
    if (step == 1) { setupStep01(); }
    if (step == 2) { setupStep02(); }
    if (step == 3) { setupStep03(); }

    if (step == 4) { setupStep04(); }
    if (step == 5) { setupStep05(); }
    if (step == 6) { setupStep06(); }
    if (step == 7) { setupStep07(); }
    
    for (int row = 0; row < ROWS; row++)
    {
        for (int column = 0; column < COLUMNS; column++)
        {
            if (maze[row][column] == 0) { fill(200); }
            if (maze[row][column] == 1) { fill(0,128,0); }
            if (maze[row][column] == 2) { fill(255,255,0); }

            rect(column*CELL_SIZE + CELL_SIZE/2, row*CELL_SIZE + CELL_SIZE/2, CELL_SIZE, CELL_SIZE);
        }
    }
}

void keyPressed() {
    if (keyCode == 37 && step != 0) {
      step--;
    }
    if (keyCode == 39) {
      step++;
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

void setupStep00()
{
    fillRow(1, new int[] { 4 });
    fillRow(2, new int[] { 2, 4, 5 });
    fillRow(3, new int[] { 1, 2, 5, 6 });
    fillRow(4, new int[] { 2, 4, 5 });
    fillRow(5, new int[] { 4 });
}

void setupStep01()
{
    fillRow(1, new int[] { 3, 5 });
    fillRow(2, new int[] { 1, 5, 6 });
    fillRow(3, new int[] { 5 });
    fillRow(4, new int[] { 1, 5, 6 });
    fillRow(5, new int[] { 3, 5 });
}

void setupStep02()
{
    fillRow(0, new int[] { 4 });
    fillRow(1, new int[] { 2, 4, 6 });
    fillRow(2, new int[] { 2 });
    fillRow(3, new int[] { 0, 1, 2, 4, 5, 7 });
    fillRow(4, new int[] { 2 });
    fillRow(5, new int[] { 2, 4, 6 });
    fillRow(6, new int[] { 4 });
}

void setupStep03()
{
    fillRow(0, new int[] { 3, 5 });
    fillRow(1, new int[] { 1, 5 });
    fillRow(2, new int[] { 0, 4, 6, 7 });
    fillRow(3, new int[] { 1, 6 });
    fillRow(4, new int[] { 0, 4, 6, 7 });
    fillRow(5, new int[] { 1, 5 });
    fillRow(6, new int[] { 3, 5 });
}

void setupStep04()
{
    fillRow(0, new int[] { 2, 4, 6 });
    fillRow(1, new int[] { 0, 2, 3, 7 });
    fillRow(2, new int[] { 1, 2 });
    fillRow(3, new int[] { 0, 3, 4, 6 });
    fillRow(4, new int[] { 1, 2 });
    fillRow(5, new int[] { 0, 2, 3, 7 });
    fillRow(6, new int[] { 2, 4, 6 });
}

void setupStep05()
{
    fillRow(0, new int[] { 1, 5, 7 });
    fillRow(1, new int[] { 2, 3, 4, 5, 6 });
    fillRow(2, new int[] { 0, 1, 2, 4, 5, 6, 7 });
    fillRow(3, new int[] { 5 });
    fillRow(4, new int[] { 0, 1, 2, 4, 5, 6, 7 });
    fillRow(5, new int[] { 2, 3, 4, 5, 6 });
    fillRow(6, new int[] { 1, 5 });  // Simetric loose
}

void setupStep06()
{
    fillRow(0, new int[] { 2, 3 });
    fillRow(1, new int[] { 0, 2, 3, 4, 5, 6 });
    fillRow(2, new int[] { 4, 5, 6 });
    fillRow(3, new int[] { 5 });
    fillRow(4, new int[] { 4, 5, 6 });
    fillRow(5, new int[] { 0, 2, 3, 4, 5, 6, 7 });
    fillRow(6, new int[] { 2, 3, 6 });
}

void setupStep07()
{
    fillRow(0, new int[] { 1, 3, 5, 6 });
    fillRow(1, new int[] { 1, 3, 4, 5, 7 });
    fillRow(2, new int[] { 1, 2, 4, 5, 6, 7 });
    fillRow(3, new int[] { 3, 5, 7 });
    fillRow(4, new int[] { 1, 2, 4, 5, 6, 7 });
    fillRow(5, new int[] { 1, 3, 4, 5, 6 });
    fillRow(6, new int[] { 1, 3 });
}
