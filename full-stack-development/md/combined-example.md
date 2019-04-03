
## Combine features

Combine Records, Pattern matching, Discriminated unions into program.

---

### Scenario

You have £1000 to put into a bank account to get the most at the end of 10 years. 

There are different bank accounts, with different APR rates. Some of the accounts give a free amounts as a joining gift.

Want to calculate the best deal...

---?code=code/all/Demo/Program.fs&lang=fs

@[31-40](Define units of measure)
@[43-53](Bank info and record)
@[55-67](Function to parse csv line or None)
@[59-60,67](Matches to array with 3 parts)
@[56-57,61-62,66](Matches to successful float and int parsing)
@[52-53,63-65](Bank info record)
@[85-88](Parse csv to bank infos)
@[69-75](Function to compound interest, takes rate and years as args)
@[77-80](Function to calculate amount after years)
@[90-101](Calculate amounts for bank infos)

---

@quote[and now for something completely different.]
