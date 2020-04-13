import pandas as pd
import pyodbc
import sqlalchemy

df = pd.DataFrame({"Id": [1, 2, 3, 4], "Name": ["Sid", "Ram", "Shyam", "Sam"]});
engine = sqlalchemy.create_engine("mssql+pyodbc://scott:tiger@some_dsn")
df.to_sql()
