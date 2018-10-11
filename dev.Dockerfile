FROM ruby:2.3.3
#
RUN apt-get update && apt-get install -y \
#Packages
net-tools \
nodejs \
netcat 

RUN apt-get clean

#Install gems
RUN mkdir /app
WORKDIR /app
COPY Gemfile* /app/
RUN bundle install

#Upload source
COPY . /app
RUN useradd ruby
RUN chown -R ruby /app
USER ruby

# Database defaults
ENV DATABASE_NAME bookit
ENV DATABASE_HOST db
ENV DATABASE_USER bookit
ENV DATABASE_PASSWORD iamapassword
ENV DATABASE_ADAPTER mysql2

# Start server
ENV RAILS_ENV development
ENV RACK_ENV development
ENV SECRET_KEY_BASE secret
ENV PORT 3000
EXPOSE 3000


CMD ["sh", "start.sh"]
