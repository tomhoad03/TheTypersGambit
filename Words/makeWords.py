# Take file of 20k words, output 2 files: smallWords (3-5) and largeWords (7-9)

words = open("words.txt", 'r')
s = open("sWords.txt", 'w')
l = open("lWords.txt", 'w')

for word in words:
    myLen = len(word)
    if (myLen > 3 and myLen < 7):
        s.write(word)
    if (myLen > 8 and myLen < 11):
        l.write(word)
