import React, { useState, useEffect } from "react";
import { GetListOfNewsCategories } from "../../Services/CategoryService/category.services";
import { Category } from "../../Models/Category";
import { CategoriesList } from "./categoriesList.component";
import "./categories.css";
import { useAuth0 } from "@auth0/auth0-react";
import {
  getUserCategories,
  LoginUser,
  UpdateUserCategoriesInDB,
} from "../../Services/UserService/user.services";
import { Users } from "../../Models/Users";

export const Categories = () => {
  const [categoriesList, setCategoriesList] = useState<Category[]>([]);
  const [selectedCategories, setSelectedCategories] = useState<Category[]>([]);
  const { user } = useAuth0();
  const newsWebsites = ["walla", "ynet", "maariv", "globes"];
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const InitUserDB = async () => {
    let email: string | undefined = user?.email;
    await LoginUser(email);
  };

  useEffect(() => {
    InitUserDB();
    setTimeout(() => {
      FetchCategories();
    }, 1000); // 1 second delay
    setTimeout(() => {
      FetchUserCategories();
    }, 2000); // 2 second delay
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const FetchCategories = async () => {
    setIsLoading(true);
    try {
      const response: Category[] = await GetListOfNewsCategories();
      console.log(response);
      if (response) {
        setCategoriesList(response);
      }
    } catch (error) {
      console.error(error);
    }
    setIsLoading(false);
  };

  const FetchUserCategories = async () => {
    setIsLoading(true);
    try {
      let email: string | undefined = user?.email;
      const userCategories = await getUserCategories(email);
      setSelectedCategories(userCategories);
    } catch (error) {
      console.log(error);
    }
    setIsLoading(false);
  };

  const handleSelectedCategoriesChange = (categories: Category[]) => {
    setSelectedCategories(categories);
  };

  const UpdateUser = async () => {
    setIsLoading(true);
    const categoriesCount = 3;
    if (!user?.email) {
      console.log("User email is not defined");
      return;
    }
    if (selectedCategories.length !== categoriesCount) {
      alert(`Please choose exactly ${categoriesCount} categories`);
      return;
    }
    try {
      let putUser: Users = {
        id: 0,
        email: user?.email,
        firstCategoryID: selectedCategories[0].id,
        secondCategoryID: selectedCategories[1].id,
        thirdCategoryID: selectedCategories[2].id,
      };
      await UpdateUserCategoriesInDB(putUser);
    } catch (error) {
      console.log(error);
      setError("Failed to update user categories.");
    }
    setIsLoading(false);
  };

  return (
    <div className="categories-container">
      <button className="confirm-categories-btn" onClick={() => UpdateUser()}>
        Confirm Categories
      </button>
      {isLoading && <div>Loading...</div>}
      {error && <div>{error}</div>}
      <div className="checkbox-container">
        {newsWebsites.map((newsWebsite) => (
          <div key={newsWebsite}>
            {
              <CategoriesList
                categories={categoriesList}
                newsWebsiteName={newsWebsite}
                selectedCategories={selectedCategories}
                onSelectedCategoriesChange={handleSelectedCategoriesChange}
              ></CategoriesList>
            }
          </div>
        ))}
      </div>
    </div>
  );
};
