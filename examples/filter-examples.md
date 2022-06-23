# Diffrent examples of how filters and selection expressions can be combined

## Without any filter
### All values for Region
```Region=*``` => ```00, 01, 0114, 0115, 0117, 0120, 0123, 0125, 0126, 0127, 0128, 0136, 0138, 0139, 0140, 0160, 0162, 0163, 0180, 0181, 0182, 0183, 0184, 0186, 0187, 0188, 0191, 0192, 03, 0305, 0319, 0330, 0331, 0360, 0380, 0381, 0382, 04, 0428, 0461, 0480, 0481, 0482, 0483, 0484, 0486, 0488, 05, 0509, 0512, 0513, 0560, 0561, 0562, 0563, 0580, 0581, 0582, 0583, 0584, 0586, 06, 0604, 0617, 0642, 0643, 0662, 0665, 0680, 0682, 0683, 0684, 0685, 0686, 0687, 07, 0760, 0761, 0763, 0764, 0765, 0767, 0780, 0781, 08, 0821, 0834, 0840, 0860, 0861, 0862, 0880, 0881, 0882, 0883, 0884, 0885, 09, 0980, 10, 1060, 1080, 1081, 1082, 1083, 12, 1214, 1230, 1231, 1233, 1256, 1257, 1260, 1261, 1262, 1263, 1264, 1265, 1266, 1267, 1270, 1272, 1273, 1275, 1276, 1277, 1278, 1280, 1281, 1282, 1283, 1284, 1285, 1286, 1287, 1290, 1291, 1292, 1293, 13, 1315, 1380, 1381, 1382, 1383, 1384, 14, 1401, 1402, 1407, 1415, 1419, 1421, 1427, 1430, 1435, 1438, 1439, 1440, 1441, 1442, 1443, 1444, 1445, 1446, 1447, 1452, 1460, 1461, 1462, 1463, 1465, 1466, 1470, 1471, 1472, 1473, 1480, 1481, 1482, 1484, 1485, 1486, 1487, 1488, 1489, 1490, 1491, 1492, 1493, 1494, 1495, 1496, 1497, 1498, 1499, 17, 1715, 1730, 1737, 1760, 1761, 1762, 1763, 1764, 1765, 1766, 1780, 1781, 1782, 1783, 1784, 1785, 18, 1814, 1860, 1861, 1862, 1863, 1864, 1880, 1881, 1882, 1883, 1884, 1885, 19, 1904, 1907, 1960, 1961, 1962, 1980, 1981, 1982, 1983, 1984, 20, 2021, 2023, 2026, 2029, 2031, 2034, 2039, 2061, 2062, 2080, 2081, 2082, 2083, 2084, 2085, 21, 2101, 2104, 2121, 2132, 2161, 2180, 2181, 2182, 2183, 2184, 22, 2260, 2262, 2280, 2281, 2282, 2283, 2284, 23, 2303, 2305, 2309, 2313, 2321, 2326, 2361, 2380, 24, 2401, 2403, 2404, 2409, 2417, 2418, 2421, 2422, 2425, 2460, 2462, 2463, 2480, 2481, 2482, 25, 2505, 2506, 2510, 2513, 2514, 2518, 2521, 2523, 2560, 2580, 2581, 2582, 2583, 2584```

### All values for region starting with 01
```Region=01*``` => ```01, 0114, 0115, 0117, 0120, 0123, 0125, 0126, 0127, 0128, 0136, 0138, 0139, 0140, 0160, 0162, 0163, 0180, 0181, 0182, 0183, 0184, 0186, 0187, 0188, 0191, 0192```

## With filter and aggregated values
### All values remaining for region starting with 01 after having applied fitler
```Region@vs_RegionLän07=01*``` => ```01```

### All values remaining for region starting with SE1 after having applied fitler
```Region@agg_RegionNUTS2_2008=SE1*``` => ```SE11, SE12```

### All values remaining after having applied fitler
Region@vs_RegionLän07=*``` => ```01, 03, 04, 05, 06, 07, 08, 09, 10, 12, 13, 14, 17, 18, 19, 20, 21, 22, 23, 24, 25

### Combination of selection expressions, all values remaining for region starting with 01 or ending with 3 after having applied fitler
```Region@vs_RegionLän07=01*, *3``` => ```01, 03, 13, 23```

### Combination of selection expressions, all values remaining for region starting with SE1 or starting with SE and ending with 1 after having applied fitler
```Region@agg_RegionNUTS2_2008=SE1*, SE*1``` => ```SE11, SE12, SE21, SE31```

## With filter and included values 
### All values remaining for region starting with 01 after having applied fitler
```Region$vs_RegionLän07=01*``` => ```01```

### All values remaining after having applied fitler
```Region$vs_RegionLän07=*``` => ```01, 03, 04, 05, 06, 07, 08, 09, 10, 12, 13, 14, 17, 18, 19, 20, 21, 22, 23, 24, 25```

### All values remaining for region starting with SE1 after having applied fitler
```Region$agg_RegionNUTS2_2008=SE1*``` => ```01, 03, 04, 04, 18, 19```

### Combination of selection expressions, all values remaining for region starting with 01 or ending with 3 after having applied fitler
```Region$vs_RegionLän07=01*, *3``` => ```01, 03, 13, 23```

### Combination of selection expressions, all values remaining for region starting with SE1 or starting with SE and ending with 1 after having applied fitler
```Region$agg_RegionNUTS2_2008=SE1*, SE?1``` => ```01, 03, 04, 04, 18, 19, 06, 07, 08, 09, 17, 20, 21```





