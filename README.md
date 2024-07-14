# Blogify
* An ASP.NET Core Razor Pages basic blog application allows users to create, read, update, and delete (CRUD) blog posts.

**NOTES :** 
  * **THIS JUST AN TRAINING PROJECT.**
  * Sign ✅ means that feature is fully implemented.
  * Sign ⛔ means that feature is canceled or there are no future plans to implement it.
  * Sign ➖ means that feature is pending.


## Features
* **User Authentication and Authorization:**
  * **User Registration:** Allow new users to sign up with **email verification**. ➖
  * **User Login/Logout:** Secure login/logout with session management. ➖
  * **Roles and Permissions:** Different roles like Admin, Author, and Reader with varying permissions. ➖


* **Blog Post Management:**
  * **Create/Edit/Delete Posts:** Full CRUD functionality for blog posts. ➖
  * **Rich Text Editor:** Use some pre-build rich text editor. ➖
  * **Post Scheduling:** Schedule posts to be published at a future date/time. ➖
  * **Drafts:** Save drafts and preview posts before publishing. ➖


* **Categories and Tags:**
  * **Category Management:** Create, edit, and delete categories. ➖
  * **Tag Management:** Add tags to posts for better organization and search ability. ➖
  * **Category/Tag Pages:** Display all posts under a specific category or tag. ➖


* **Commenting System:**
  * **Add Comments:** Allow readers to add comments to posts. ➖
  * **Comment Moderation:** Admins can approve, delete, or mark comments as spam. ➖
  * **Nested Comments:** Enable replies to comments for threaded discussions. ➖


* **User Profiles:**
  * **Profile Page:** User-specific profile pages displaying their posts and comments. ➖
  * **Profile Editing:** Allow users to edit their profiles and upload avatars. ➖
  * **Follow System:** Users can follow other authors to get updates on their new posts. ➖


* **Search and Filtering:**
  * **Search Bar:** Implement a search bar to find posts by keywords. ➖
  * **Filter by Date/Category/Tag:** Filter posts by various criteria. ➖


* **Notifications and Subscriptions:**
  * **Email Notifications:** Notify users about new posts, comments, and replies. ➖
  * **Subscriptions:** Users can subscribe to authors or categories. ➖


* **SEO Optimization:**
  * **Meta Tags:** Automatic generation of meta tags for SEO. ➖
  * **Sitemap:** Generate a sitemap for better search engine indexing. ➖


* **Analytics and Reporting:**
  * **Post Views:** Track and display the number of views for each post. ➖
  * **User Engagement:** Monitor user interactions like comments, likes, and shares. ➖
  * **Admin Dashboard:** A dashboard with key metrics and reports for admins. ➖


## Project Structure
### Blogify.Contract
* Contains all interfaces and data transfer objects DTOs.
* _ex. Services Interfaces.
* Should only depends on *Blogify.Shared*

### Blogify.Domain
* Contains all Entities, Repositories Interfaces, and Services implementation.
* Should only depends on *Blogify.Shared*

### Blogify.EntityFrameworkCore
* Contains the ApplicationDbContext and Repositories implementation.
* Should only depends on *Blogify.Domain* 

### Blogify.Shared
* Contains constants and enums shared across by all other projects
* Should be independent project.

### Blogify.Test
* Contains all unit tests

### Blogify.


## Tech Stack
### Framework
* .NET 8  using Razor Pages
### Data Access
* Database Provider : [Sqlite](https://www.sqlite.org/)
* ORM : [Ef Core 8.0](https://learn.microsoft.com/en-us/ef/core/)
### Authn & Authz
* [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)
### UI
* Styling: [Bootstrap](https://getbootstrap.com/)
### Testing
* Unit Testing : [xUnit](https://xunit.net/)
* Mocking : [Moq](https://github.com/devlooped/moq) 