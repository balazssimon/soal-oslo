cd common
find . -name '*' | xargs wc -l
echo "----"
find . -name '*' | xargs wc -c
echo "----"
find . -type f -print | wc -l
cd ..

echo "===="

cd IbmRad
find . -name '*' | xargs wc -l
echo "----"
find . -name '*' | xargs wc -c
echo "----"
find . -type f -print | wc -l
cd ..

echo "===="

cd JBoss
find . -name '*' | xargs wc -l
echo "----"
find . -name '*' | xargs wc -c
echo "----"
find . -type f -print | wc -l
cd ..

echo "===="

cd NetBeans
find . -name '*' | xargs wc -l
echo "----"
find . -name '*' | xargs wc -c
echo "----"
find . -type f -print | wc -l
cd ..

echo "===="

cd Oracle
find . -name '*' | xargs wc -l
echo "----"
find . -name '*' | xargs wc -c
echo "----"
find . -type f -print | wc -l
cd ..

echo "===="

cd Tomcat
find . -name '*' | xargs wc -l
echo "----"
find . -name '*' | xargs wc -c
echo "----"
find . -type f -print | wc -l
cd ..

echo "===="

cd VisualStudio
find . -name '*' | xargs wc -l
echo "----"
find . -name '*' | xargs wc -c
echo "----"
find . -type f -print | wc -l
cd ..

echo "===="
