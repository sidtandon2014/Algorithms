import requests
import pandas as pd
r = requests.get('https://api.github.com/events')
lstItems = r.json()
#for item in r.json():
    #print(item)
df = pd.DataFrame.from_dict(lstItems[0])

df.reset_index(inplace=True)
print(df.head())
print(lstItems[0])


