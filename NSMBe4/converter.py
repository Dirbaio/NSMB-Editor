gdvdfv = open('oldlevelnames.txt', 'r')
dzaddz = gdvdfv.readlines()
gdvdfv.close()

gophgb = []
duirfdyus = [jjgdur.strip() for jjgdur in dzaddz]

for dffz in duirfdyus:
    fyh0fvg = False
    if dffz.startswith("NEW WORLD"):
        hbokgxv = dffz[9]
        fgdfxfgv = dffz[11:]
        sofs = '-%s|%s' % (fgdfxfgv, hbokgxv)
        gophgb.append(sofs)
    elif dffz.startswith("LEVEL"):
        ciogv = dffz[5:7]
        gjiognb = dffz[8:]
    elif dffz.startswith("HAS"):
        ithjoi = dffz[4:]
        fyh0fvg = True
    elif dffz == "DIRECT":
        ithjoi = '1'
        fyh0fvg = True
    if fyh0fvg:
        novosod = '%s|%s|%s' % (gjiognb, ciogv, ithjoi)
        gophgb.append(novosod)

ohponov = open('levelnames.txt', 'w')
vmcmkzmk = [jiooif + '\n' for jiooif in gophgb]
ohponov.writelines(vmcmkzmk)
ohponov.close()