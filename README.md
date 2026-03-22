# Scénář aplikace internetového bankovnictví – FireBank

**Platforma:** .NET C# + Avalonia framework + LiteDB
## 1. Spuštění aplikace
Po spuštění aplikace se zobrazí úvodní obrazovka.

Možnosti: Přihlášení nebo Registrace nového uživatele.

## 2. Registrace uživatele
Uživatel zvolí možnost Registrace.

Vyplní formulář: Jméno, Příjmení, Email, Uživatelské jméno, Heslo, Potvrzení hesla.

Aplikace zkontroluje, zda uživatel již neexistuje a zda se hesla shodují.

Pokud je vše v pořádku, vytvoří se nový uživatel a uloží se do databáze LiteDB.

Uživatel je poté přesměrován na přihlašovací obrazovku.

## 3. Přihlášení
Uživatel zadá uživatelské jméno a heslo.

Aplikace ověří údaje v databázi LiteDB.

Pokud jsou údaje správné, otevře se hlavní dashboard.

Pokud jsou údaje nesprávné, zobrazí se chybová hláška.

## 4. Dashboard (hlavní obrazovka)
Po přihlášení uživatel vidí přehled svých účtů, zůstatky a poslední transakce.

Menu obsahuje: Moje účty, Vytvořit účet, Nová transakce, Historie transakcí, Odhlášení.

## 5. Správa bankovních účtů
Uživatel může vytvořit nový bankovní účet.

Vybere typ účtu: běžný nebo spořící.

Systém vygeneruje číslo účtu, nastaví počáteční zůstatek a uloží účet do LiteDB.

## 6. Přehled účtů
Uživatel vidí seznam svých účtů s informacemi: číslo účtu, typ účtu a aktuální zůstatek.

Po kliknutí na účet se zobrazí detail účtu a historie transakcí.

## 7. Vytvoření transakce
Uživatel vyplní zdrojový účet, cílový účet, částku a poznámku.

Aplikace zkontroluje existenci účtů a dostatek prostředků.

Po potvrzení se částka odečte ze zdrojového účtu a přičte na cílový účet.

Transakce se uloží do databáze LiteDB.

## 8. Historie transakcí
Uživatel může zobrazit všechny své transakce nebo transakce konkrétního účtu.

Zobrazené informace: datum, částka, zdrojový účet, cílový účet a poznámka.

## 9. Odhlášení
Uživatel zvolí možnost Odhlásit.

Systém ukončí relaci a vrátí uživatele na přihlašovací obrazovku.

## Struktura kolekcí v LiteDB
Users: Id, FirstName, LastName, Email, Username, PasswordHash

Accounts: Id, UserId, AccountNumber, AccountType, Balance, Currency, CreatedAt

Transactions: Id, FromAccountId, ToAccountId, Amount, Note, Date
