# Wheat GrainClassifier WPF App

![alt text](image.png)

# Guide d'Installation et de Test - WheatGrainClassifierWpfApp

## Table des Matières

1. [Vue d'ensemble du Projet](#vue-densemble-du-projet)
2. [Prérequis Système](#prérequis-système)
3. [Installation Locale](#installation-locale)
4. [Configuration de la Base de Données](#configuration-de-la-base-de-données)
5. [Exécution de l'Application](#exécution-de-lapplication)
6. [Structure du Projet](#structure-du-projet)
7. [Dépannage](#dépannage)

---

## Vue d'ensemble du Projet

**WheatGrainClassifierWpfApp** est une application WPF (Windows Presentation Foundation) qui implémente un classificateur de grains de blé utilisant l'algorithme **K-Nearest Neighbors (KNN)**. 

### Fonctionnalités principales :
- **Classification de grains** : Classifie les variétés de blé en fonction de 7 caractéristiques morphologiques
- **Deux modes de fonctionnement** :
  - **Mode Calcul** : Effectue des prédictions sur des grains individuels
  - **Mode Expérience** : Lance des expériences KNN avec différents paramètres et enregistre les résultats
- **Métriques de distance** : Support pour la distance Euclidienne et Manhattan
- **Persistance des données** : Enregistrement des résultats des expériences dans une base de données SQL Server
- **Ensemble de données** : Utilise des fichiers CSV pour l'apprentissage et le test

---

## Prérequis Système

### Logiciels Requis :

1. **Windows 10/11** (ou Windows Server 2019+)
2. **Visual Studio 2022** ou **Visual Studio Code** avec les extensions C#
3. **.NET 10.0** (SDK et Runtime)
4. **SQL Server 2019** ou version ultérieure (Express ou supérieur)
5. **SQL Server Management Studio (SSMS)** (optionnel, pour gérer la BD)

---

## Installation Locale

### Étape 1 : Cloner ou Télécharger le Projet

```bash
# Si le projet est dans un dépôt Git
git clone https://github.com/Lil-Code30/inf1126207-tp-02-wheat-grain-classifier-wpfApp
cd inf1126207-tp-02-wheat-grain-classifier-wpfApp
```

### Étape 2 : Vérifier les Outils Nécessaires

#### Installer le SDK .NET 10.0

1. Télécharger depuis : https://dotnet.microsoft.com/download
2. Vérifier l'installation :
```powershell
dotnet --version
# Devrait afficher : 10.0.x ou supérieur
```

#### Installer/Vérifier SQL Server

1. Télécharger **SQL Server Express** : https://www.microsoft.com/fr-fr/sql-server/sql-server-editions-express
2. Installer avec les paramètres par défaut (instance : `SQLEXPRESS`)
3. Vérifier la connexion avec SQL Server Management Studio

### Étape 3 : Restaurer les Dépendances NuGet

Dans le terminal PowerShell, à la racine du projet :

```powershell
# Restaurer les dépendances NuGet
dotnet restore

# Ou dans Visual Studio :
# Menu > Tools > NuGet Package Manager > Package Manager Console
# Exécuter : Update-Package
```

Les dépendances principales à installer :
- **EntityFrameworkCore 10.0.7** : ORM pour la gestion de la base de données
- **CsvHelper 33.1.0** : Lecture/écriture des fichiers CSV
- **MahApps.Metro** : Thème visuel moderne pour WPF
- **Newtonsoft.Json** : Sérialisation JSON

---

## Configuration de la Base de Données

### Étape 1 : Adapter la Chaîne de Connexion

Ouvrir le fichier : `Db/WheatGrainClassifierDbContext.cs`

Localiser la méthode `OnConfiguring` :

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    string connectionString = "Data Source=DESKTOP-LDNM8KV\\SQLEXPRESS;Initial Catalog=WheatGrainClassifierDB;Integrated Security=True;Encrypt=False";
    string dataBaseName = "WheatGrainClassifierDB";

    optionsBuilder.UseSqlServer($"{connectionString};Database={dataBaseName}");
}
```

**Adapter les paramètres suivants selon votre configuration locale :**

| Paramètre | Description | Exemple |
|-----------|-------------|---------|
| `Data Source` | Nom du serveur SQL Server | `DESKTOP-LDNM8KV\\SQLEXPRESS` ou `localhost\\SQLEXPRESS` ou `.\\SQLEXPRESS` |
| `Initial Catalog` | Nom de la base de données | `WheatGrainClassifierDB` |
| `Integrated Security` | Authentification Windows | `True` (recommandé) |
| `Encrypt` | Chiffrement de la connexion | `False` (pour développement local) |

**Exemple pour un serveur local :**
```csharp
string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=WheatGrainClassifierDB;Integrated Security=True;Encrypt=False";
```

**Exemple avec authentification SQL Server :**
```csharp
string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=WheatGrainClassifierDB;User Id=sa;Password=VotreMotDePasse;Encrypt=False";
```

### Étape 2 : Vérifier le Serveur SQL Server

#### Trouver le nom de votre serveur SQL Server :

1. Ouvrir **SQL Server Management Studio**
2. À la connexion, le serveur s'affiche dans la dropdown (ex. : `DESKTOP-LDNM8KV\\SQLEXPRESS`)
3. Ou dans PowerShell :
```powershell
# Liste les instances SQL Server
sqlcmd -L
```

### Étape 3 : Appliquer les Migrations

**Important** : Les migrations créent automatiquement la base de données et les tables.

Dans la **Package Manager Console** de Visual Studio :

```powershell
# Dans le dossier du projet
Update-Database
```

Ou avec la CLI .NET :

```powershell
dotnet ef database update --project WheatGrainClassifierWpfApp.csproj
```

**Que fait cette commande :**
- Crée la base de données `WheatGrainClassifierDB` sur le serveur SQL Server
- Crée la table `Experiments` pour stocker les résultats
- Applique tous les paramètres de formatage et de migration

### Étape 4 : Vérifier la Base de Données

Ouvrir **SQL Server Management Studio** et vérifier :
1. Base de données `WheatGrainClassifierDB` existe
2. Table `Experiments` existe avec les colonnes :
   - `Id`, `KValue`, `DistanceValue`, `Accuracy`, `TrainSize`, `TestSize`, `ExecutionDate`, `UserId`, `UserName`

---

## Exécution de l'Application

### Démarrage via Visual Studio

1. Ouvrir `WheatGrainClassifierWpfApp.slnx` dans Visual Studio 2022
2. Cliquer sur le bouton **Run** (Play ▶️) ou appuyer sur `F5`
3. L'application démarre avec l'interface graphique

### Démarrage via CLI

```powershell
# À la racine du projet
dotnet run
```

### Au Démarrage

- L'interface charge automatiquement les données d'entraînement (`Data/seeds_dataset_training.csv`)
- Les données de test sont disponibles (`Data/seeds_dataset_test.csv`)
- Deux onglets sont proposés : **Calcul** et **Expérience**

---

### Données de Test

Les fichiers CSV contiennent des grains avec les colonnes :
```
Area,Perimeter,Compactness,Kernel_Length,Kernel_Width,Asymmetry_Coefficient,Groove_Length,Class
15.26,14.84,0.8707,5.763,3.312,2.221,5.22,Kama
...
```

**Variétés de blé** (classes) :
- **Kama**
- **Rosa**
- **Canadian**

---

## Structure du Projet

```
WheatGrainClassifierWpfApp/
├── Models/                      # Modèles de données
│   ├── Grain.cs                # Caractéristiques d'un grain
│   ├── Experiment.cs           # Résultats d'une expérience
│   ├── Voisin.cs               # Voisin KNN
│   └── ConfusionRow.cs         # Matrice de confusion
│
├── Services/                    # Logique métier
│   ├── KnnService.cs           # Implémentation du KNN
│   ├── PerformanceService.cs   # Calcul de précision
│   └── ApiService.cs           # Services API
│
├── ViewModels/                 # Modèles de vue (MVVM)
│   ├── MainViewModel.cs        # Navigation principale
│   ├── CalculationViewModel.cs # Vue prédiction
│   └── ExperienceViewModel.cs  # Vue expérience
│
├── Views/                      # Interfaces graphiques XAML
│   ├── MainWindow.xaml         # Fenêtre principale
│   ├── CalculationView.xaml    # Vue calcul
│   └── ExperienceView.xaml     # Vue expérience
│
├── Db/                         # Accès à la base de données
│   └── WheatGrainClassifierDbContext.cs  # DbContext EF Core
│
├── Migrations/                 # Migrations de base de données
│   └── ...
│
├── Helpers/                    # Classes utilitaires
│   ├── CsvUtils.cs             # Lecture CSV
│   ├── EuclideanDistance.cs    # Distance Euclidienne
│   ├── ManhattanDistance.cs    # Distance Manhattan
│   └── TriRapide.cs            # Tri rapide (QuickSort)
│
├── Interfaces/                 # Contrats d'interfaces
│   ├── IDistance.cs            # Interface pour les distances
│   └── IAlgorithmeTri.cs       # Interface pour les algorithmes de tri
│
├── Commands/                   # Commandes WPF/MVVM
│   └── RelayCommand.cs         # Implémentation des commandes
│
├── Data/                       # Données d'entraînement/test
│   ├── seeds_dataset_training.csv
│   └── seeds_dataset_test.csv
│
├── WheatGrainClassifierWpfApp.csproj  # Configuration du projet
├── App.xaml & App.xaml.cs             # Point d'entrée application
└── README.md
```

### Fichiers Clés

| Fichier | Rôle |
|---------|------|
| `Db/WheatGrainClassifierDbContext.cs` | Configuration de la BD et chaîne de connexion |
| `Services/KnnService.cs` | Algorithme KNN principal |
| `ViewModels/CalculationViewModel.cs` | Logique de prédiction manuelle |
| `ViewModels/ExperienceViewModel.cs` | Logique des expériences |
| `Data/*.csv` | Données d'entraînement et test |

---

## Dépannage

### Erreur 1 : "Cannot connect to SQL Server"

**Cause** : La chaîne de connexion est incorrecte ou SQL Server n'est pas accessible.

**Solutions** :

1. Vérifier que SQL Server s'exécute :
```powershell
# Vérifier le service SQL Server
Get-Service MSSQLSERVER | Select Status

# Démarrer SQL Server si arrêté
Start-Service MSSQLSERVER
```

2. Trouver le nom exact du serveur :
```powershell
sqlcmd -L
```

3. Adapter la chaîne dans `Db/WheatGrainClassifierDbContext.cs` :
   - Pour `localhost` : `Data Source=.\\SQLEXPRESS`
   - Pour un autre serveur : `Data Source=NOM_SERVEUR\\INSTANCE`

4. Tester la connexion avec SSMS

### Erreur 2 : "Database does not exist"

**Cause** : Les migrations n'ont pas été appliquées.

**Solution** :

```powershell
# Dans Package Manager Console
Update-Database

# Ou avec CLI
dotnet ef database update
```

### Erreur 3 : "CSV file not found"

**Cause** : Les fichiers de données ne sont pas dans le dossier `Data/`.

**Solution** :

1. Vérifier que les fichiers existent :
   - `Data/seeds_dataset_training.csv`
   - `Data/seeds_dataset_test.csv`

2. Si manquants, télécharger depuis l'UCI Machine Learning Repository ou les restaurer depuis Git

### Erreur 4 : "NuGet packages not restored"

**Solution** :

```powershell
# Restaurer tous les packages
dotnet restore

# Ou dans Visual Studio Package Manager
Update-Package
```

### Erreur 5 : ".NET SDK not found"

**Solution** :

1. Vérifier l'installation du SDK .NET :
```powershell
dotnet --version
```

2. Si absent, télécharger et installer : https://dotnet.microsoft.com/download/dotnet/10.0

3. Redémarrer Visual Studio après installation

### Erreur 6 : "Migration conflict or drift"

**Cause** : La base de données a été modifiée manuellement.

**Solution** :

1. Réinitialiser la base de données :
```powershell
# Dans Package Manager Console
Remove-Database
Update-Database
```

2. Ou via CLI :
```powershell
dotnet ef database drop
dotnet ef database update
```

### Vérification de la Configuration

Pour s'assurer que tout fonctionne correctement :

1. ✅ `.NET SDK` installé : `dotnet --version`
2. ✅ SQL Server en cours d'exécution : `Get-Service MSSQLSERVER`
3. ✅ Dépendances restaurées : `dotnet restore`
4. ✅ Migrations appliquées : Vérifier base de données dans SSMS
5. ✅ Fichiers CSV présents : Vérifier dossier `Data/`

---

## Ressources et Documentation

- **Microsoft Docs - WPF** : https://docs.microsoft.com/en-us/dotnet/framework/wpf/
- **Entity Framework Core** : https://docs.microsoft.com/en-us/ef/core/
- **K-Nearest Neighbors** : https://en.wikipedia.org/wiki/K-nearest_neighbors_algorithm
- **Seeds Dataset** : https://archive.ics.uci.edu/ml/datasets/seeds
- **.NET 10.0** : https://dotnet.microsoft.com/download/dotnet/10.0

---

**Dernière mise à jour** : Mai 2026
**Version du projet** : .NET 10.0-windows, SQL Server Express
**Statut** : ✅ En Locale
