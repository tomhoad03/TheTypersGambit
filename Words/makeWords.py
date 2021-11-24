# 10,000 english words, without swears
# https://github.com/first20hours/google-10000-english/blob/master/google-10000-english-no-swears.txt
# smallWords (3-5) and largeWords (7-9)

words = open("noSwearWords.txt", 'r')
s = open("noSwearSmallWords.txt", 'w')
l = open("noSwearLargeWords.txt", 'w')

for word in words:
    myLen = len(word)
    if (myLen > 3 and myLen < 7):
        s.write(word)
    if (myLen > 8 and myLen < 11):
        l.write(word)
