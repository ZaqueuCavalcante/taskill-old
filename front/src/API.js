const URL = "http://localhost:5000";
const JWT =
  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3MDU4ZWQxMC1iY2Y4LTQ4NTQtOTBmYy0wZmUzNGY0ZmE4YWUiLCJzdWIiOiIxIiwicHJlbWl1bSI6InRydWUiLCJuYmYiOjE2NzY5OTA4MDUsImV4cCI6MTY3Njk5NDQwNSwiaWF0IjoxNjc2OTkwODA1LCJpc3MiOiJ0YXNraWxsLWFwaS1kZXZlbG9wbWVudCIsImF1ZCI6InRhc2tpbGwtYXBpLWRldmVsb3BtZW50In0.R1vFG7UCTJVV9v4eEMlYdBIfK24EeKYQgGmY_vZmj60";

const AUTH_HEADER = {
  headers: { Authorization: `Bearer ${JWT}` },
};

async function getTaskById(id) {
  var response = await fetch(`${URL}/tasks/${id}`, {
    headers: { Authorization: `Bearer ${JWT}` },
    method: "GET",
  });
  console.log(AUTH_HEADER);

  console.log(response);
  var json = await response.json();
  console.log(json);
  return json;
}

export default getTaskById;
