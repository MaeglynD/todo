# Add Item:
    -add -title "TASK TITLE" -due "20/08/2020" -completed "TRUE"

# Edit items using ID's:
    -edit "1, 3, 5" -title "TASK TITLE" -due "20/08/2020" -completed "FALSE"


# Delete items using ID's:
    -delete "1, 3, 5"

# Sort table using ID's:
    -sort -id         -desc
          -title
          -due
          -completed
