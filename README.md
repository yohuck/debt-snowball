# Snowball Out Of Debt

[Deployed Application](https://debt-snowball-app-20231129131825.delightfulpebble-75fd41eb.eastus.azurecontainerapps.io/)

## Description

This is a Debt Snowball Calculator built as my submission to the [The Great .NET 8 Hack](https://github.com/microsoft/hack-together-dotnet).

The average American carries $7,951 in credit card debt. The average credit card interest rate in America today is 24.46%. That’s $162.07 lost each month per American.

This calculator is built to help user's understand their debt, how much interest they're paying, and how much they can save by paying more than their minimum payment.

The long-term goal is to essentially build Duolingo for financial education - a place where users can organize their financial future and track their progress. For the sake of the hackathon, I cut the scope to just understanding Credit Card debt and using the Snowball method where users pay off their lowest card first, then roll that card's payments into their next-highest card until their debt is paid off.

## Tools

This project was built entirely in .NET Blazor with C#. Here are some additonal tools and dependencies:

- TValue REST API (for amortization calculations)
- Azure OpenAI NuGet Package
- TailwindCSS
- Entity Framework
- Azure Container Apps
- Azure SQL Database

## Demos & Mockups

I have added a quick demo video [on TikTok.](https://www.tiktok.com/@eric.lake/video/7308833997520473375)

## Future Development

I have many features I'd like to continue building including:

- A payment flow for users to track their progress
- A text-reminder microservice to input their payments and stay on track
- Data visualization to help users see the difference their efforts make
- Adding additional financial calculators
- Add flows for edit/delete debt and snowballs
- Continued styling improvements

### Contributors:

- GitHub: [Eric Lake](https://github.com/yohuck)
