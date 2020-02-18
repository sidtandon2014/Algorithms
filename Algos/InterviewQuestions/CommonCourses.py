import numpy as np
import pandas as pd
desired_width=320

pd.set_option('display.width', desired_width)

np.set_printoptions(linewidth=desired_width)

pd.set_option('display.max_columns',10)

dict = {
    "17": "Data Structures",
    "18": "Maths",
    "19": "Data Structures"
}

df = pd.DataFrame(list(dict.items()))
df.columns = ["StudentId", "Name"]
df.loc[:, "Id"] = 1

crossjoin = df.merge(df, on="Id", how="inner")
crossjoin = crossjoin[crossjoin["Name_x"] == crossjoin["Name_y"]]  # .drop_duplicates()
crossjoin = crossjoin[crossjoin["StudentId_x"] != crossjoin["StudentId_y"]]

crossjoin.loc[:, "MergedIds"] = np.where(crossjoin["StudentId_x"] < crossjoin["StudentId_y"]
                                         , crossjoin["StudentId_x"] + "," + crossjoin["StudentId_y"]
                                         , crossjoin["StudentId_y"] + "," + crossjoin["StudentId_x"])

crossjoin.loc[:, "MergedCourses"] = np.where(crossjoin["StudentId_x"] < crossjoin["StudentId_y"]
                                         , crossjoin["Name_x"] + "," + crossjoin["Name_y"]
                                         , crossjoin["Name_y"] + "," + crossjoin["Name_x"])

crossjoin = crossjoin.loc[:,["MergedIds","MergedCourses"]].drop_duplicates()
print(crossjoin)
