Commands:

docker network create -d bridge squad-for-fun-network

docker build -t manavarro/minave.app:1.0 .

docker run -itd -p 2000:80 --network=squad-for-fun-network manavarro/minave.app:1.0 --name minaveapp

docker build -t manavarro/minave.functions:1.0 .

docker run -itd -p 1500:80 --network=squad-for-fun-network manavarro/minave.functions:1.0 --name minavefunctions