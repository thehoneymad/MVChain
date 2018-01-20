# MVChain
MVChain is a small CLI app that teaches you about a minimum viable blockchain core. Currently it demonstrates:

1. A set of private-public keypair generation for an user along with a bitcoin address.
2. A genesis block to start with. 

### To be done
3. A small demo of distributed consensus among a set of nodes.


## Build 

Install dotnet-core SDK from [here](https://www.microsoft.com/net/download/macos)
```
git clone https://github.com/thehoneymad/MVChain.git
cd MVChain/src
dotnet build
```
To publish

```
dotnet publish
```

To publish self-contained apps

```
dotnet publish -c Release -r win10-x64
dotnet publish -c Release -r osx.10.11-x64
```

# Recommened Courses and Reads
* [Coursera](https://www.coursera.org/learn/cryptocurrency/home/welcome) - Highly suggested, the deck is borrowed from the actual Princeton course.
* [Further curated reading](https://thehoneymad.gitbooks.io/daily-reading-log/content/Blockchain.html)
* [Session 1 Deck](https://docs.google.com/presentation/d/16NOT-U4dYpNKgCV9qHLNlO0N1TgEaQLZc7lwQBKLc4Q/edit?usp=sharing)
