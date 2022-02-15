# UnBookIT

A service for removing bookings from bookIT

## Configuration

The service can be configured at startup with a bunch of environment variables.

### Database configuration

#### `DATABASE_SERVER`

The host name of the database. Default is `db`.

#### `DATABASE_USER`

The username for the database. Default is `bookIT`.

#### `DATABASE_PASSWORD`

The password for the database. Default is `password`.

#### `DATABASE_NAME`

The name of the database. Default id `bookIT`.

#### `DATABASE_VERSION`

The version of the database. Default is `10.6.4-mariadb`, which is correct for
the docker image `mariadb:10`.

### Gamma configuration

#### `GAMMA_DOMAIN`

The base URL to Gamma. Default is `http://localhost:8081`.

#### `GAMMA_CALLBACK_PATH`

The path to which Gamma should return the auth code. Default is
`/auth/account/callback`, this can be change freely as long as it matches the
client configuration in Gamma.

#### `GAMMA_CLIENT_ID`

The client id configured in Gamma. Default is `id`.

#### `GAMMA_CLIENT_SECRET`

The client secret configured in Gamma. Default is `secret`.

### Response configuration

#### `REDIRECT_URL`

The full URL to which users are redirected to after successful operations.
