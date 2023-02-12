const URL = "http://localhost:5000";
const JWT =
  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIyOWM5M2ExMC01MWM3LTQ2ZDUtYjQ2Yi1jYTQwMGFlMzgwYmQiLCJzdWIiOiIxIiwibmJmIjoxNjc2MjIyOTI3LCJleHAiOjE2NzYyMjY1MjcsImlhdCI6MTY3NjIyMjkyNywiaXNzIjoidGFza2lsbC1hcGktZGV2ZWxvcG1lbnQiLCJhdWQiOiJ0YXNraWxsLWFwaS1kZXZlbG9wbWVudCJ9.bLcO510NgQ3YlPJkhQAyMWeU_PJ6kF8j6Sv3vR_L02A";

const AUTH_HEADER = {
  headers: { Authentication: `Bearer ${JWT}` },
};

async function getTaskById(id) {
  var response = await fetch(`${URL}/task/${id}`, AUTH_HEADER);
  return await response.json();
}

export default getTaskById;
