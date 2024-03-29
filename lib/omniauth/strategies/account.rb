require 'base64'
module OmniAuth
  module Strategies
    class Account < OmniAuth::Strategies::OAuth2
      option :name, :account

      option :client_options, {
        site: Rails.configuration.account_ip,
        authorize_url: "#{Rails.configuration.account_redirect}/api/oauth/authorize",
        token_url: "#{Rails.configuration.account_ip}/api/oauth/token"
      }

      uid do
        raw_info['cid']
      end

      def build_access_token
        options.token_params.merge!(:headers => {
            'Authorization' => basic_auth_header,
            'Content-Type' => "application/x-www-form-urlencoded"
        })
        super
      end

      def basic_auth_header
        "Basic " + Base64.strict_encode64("#{options[:client_id]}:#{options[:client_secret]}")
      end

      info do
        { name: raw_info["nick"] }
      end

      def raw_info
        @raw_info ||= access_token.get('/api/users/me.json').parsed
      end
    end
  end
end
