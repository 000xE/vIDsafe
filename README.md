<h1 align="center">
  vIDsafe
</h1>

<p align="center">
  A password manager focused on the idea of compartmentalisation.
</p>

<p align="center">
  <a style="text-decoration:none" href="https://github.com/outerme/1808827-FYP/releases">
    <img src="https://img.shields.io/github/v/release/outerme/1808827-FYP?include_prereleases" alt="Releases" />
  </a>
  <a style="text-decoration:none" href="https://github.com/outerme/1808827-FYP/actions">
    <img src="https://github.com/outerme/1808827-FYP/workflows/CI/badge.svg" alt="Tests" />
  </a>
  <a href="https://codecov.io/gh/outerme/1808827-FYP">
    <img src="https://codecov.io/gh/outerme/1808827-FYP/branch/main/graph/badge.svg?token=li7YnXYBZb" alt="Codecov" />
  </a>   
  <a style="text-decoration:none" href="https://www.codefactor.io/repository/github/outerme/1808827-fyp">
    <img src="https://www.codefactor.io/repository/github/outerme/1808827-fyp/badge" alt="CodeFactor" />
  </a>
</p>

## Specification

### Functional requirements

- [x] Master account:
	- [x] Allow the user to create a master account
		- [x] Name
		- [x] Password
		~~- [ ] Password hint~~
	- [x] Allow the user to login to their master account
	- [x] Allow the user to manage their master account
		- [x] Delete account
		- [x] Wipe identities
		- [x] Purge credentials
- [x] Vault:
	- [x] Identities:	
		- [x] Allow the user to create/manage identities/profiles based on their needs
			- [x] Name
			- [x] Email
			- [x] Usage/Needs
		~~- [ ] Allow the user to search for public information of their identities~~
	- [x] Credentials:
		- [x] Allow the user to generate or store and manage credentials and assign them to the identities
			- [x] Identity
			- [x] Username
			- [x] Password
			- [x] URL
		- [x] Allow the user to generate passwords and passphrases
		- [x] Allow the user to see a history of previously generated passwords
		- [x] Allow the user to check for compromised credentials
	- [x] General:
		- [x] Display an overview of the user's vault
			- [x] Identity health scores
			- [x] Weak credentials
			- [x] Security alerts
		- [x] Encrypt the vault using the methods bcrypt and AES
		- [x] Allow the user to import and export their vault in JSON/csv format
- [x] Application:
	- [x] Allow the user to change the application's settings
	- [x] Include validation for inputs
	- [x] Display error messages

### Non-functional requirements

- [x] Ensure the encryption/decryption of the vault is optimally fast and secure.
- [x] Ensure the user interface is clear, concise, and pleasant to use
- [x] Ensure the application is easy to use

## The minutes of meetings

### Tutor meetings

- 14/10/2020: 30 minutes - Discussed the synopsis brief.
- 22/10/2020: 1 hour - Presented and discussed project ideas and their strengths and weaknesses.
- 29/10/2020: 1 hour - Discussed and reviewed various group synopses.
- 05/11/2020: 1 hour - Presented the refined project ideas in short and discussed what research was done to support them.
- 19/11/2020: 1 hour - Presented some background research of the idea.
- 26/11/2020: 1 hour - Reviewed the background research and designs made for the projects.
- 03/12/2020: 30 minutes - Presented refined background research and back-end designs.
- 10/12/2020: 30 minutes - Discussed ethical approval and presented some progress with the projects.
- 17/12/2020: 1 hour 30 minutes - Demonstrated some implementations and designs of the projects.
- 14/01/2021: 1 hour - Demonstrated some implementations of the projects
- 22/01/2021: 1 hour - Demonstrated some implementations of the projects
- 28/01/2021: 1 hour - Demonstrated some implementations of the projects
- 04/02/2021: 1 hour - Demonstrated some implementations of the projects and discussed evaluation methods
- 11/02/2021: 1 hour - Mock presentations
- 15/02/2021: 1 hour - Mock presentations
- 18/02/2021: 30 minutes - Discussed testing and evaluation and presentations of the projects
- 25/02/2021: 1 hour  - Discussed evaluation and the reports of the projects
- 04/03/2021: 1 hour  - Discussed abstract and introduction of the reports of the projects

### 1-to-1 meetings

- 12/11/2020: 15 minutes - Reviewed personal synopsis for the project and obtained feedback for the feedback form.
