import React from "react";
import { Article } from "../../Models/Article";
import {
  AddOrUpdateUserClicks,
  UpdateArticleClick,
} from "../../Services/ArticleService/article.services";
import { useAuth0 } from "@auth0/auth0-react";
import "./articleTemplate.css";

interface ArticleTemplateProps {
  article: Article;
}

export const ArticleTemplate = ({ article }: ArticleTemplateProps) => {
  const { user } = useAuth0();
  const handleLinkClick = () => {
    let email = user?.email;
    if (email) {
      UpdateArticleClick(article);
      setTimeout(() => {
        AddOrUpdateUserClicks(article, email);
      }, 2000); // 2 second delay
    }
  };
  return (
    <div className="article-template-container" dir="rtl">
      <div className="article-title">{article.title}</div>
      <div className="article-description">
        <span>
          <img className="article-image" src={article.image} alt="" />
        </span>
        <span>
          <span className="article-content several-line-ellipsis">
            {article.description}
          </span>
        </span>
      </div>
      <div className="article-link">
        <button onClick={handleLinkClick} className="article-link-btn">
          <a target="_blank" rel="noreferrer" href={article.link}>
            {" "}
            Link To Article Source
          </a>
        </button>
      </div>
    </div>
  );
};
