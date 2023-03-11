import React from "react";
import { Article } from "../../Models/Article";
import "./articleTemplate.css";

interface ArticleTemplateProps {
  article: Article;
}

export const ArticleTemplate = ({ article }: ArticleTemplateProps) => {
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
        <a href={article.link}>
          <button className="article-link-btn"> Link To Article Source</button>
        </a>
      </div>
    </div>
  );
};
