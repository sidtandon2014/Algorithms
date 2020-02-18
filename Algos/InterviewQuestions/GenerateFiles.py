import pandas as pd

fileLoc = "./RawToTranInput.csv"
data = pd.read_csv(fileLoc, sep="|")

query = data["QueryFileName"].unique()

for item in query:
    path = "./Queries/" + item
    f = open(path, "w+")
    f.write('queryKey=""""""')
    f.close()
