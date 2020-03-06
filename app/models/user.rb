# == Schema Information
#
# Table name: users
#
#  cid        :string(255)      primary key
#  first_name :string(255)
#  last_name  :string(255)
#  nick       :string(255)
#  mail       :string(255)
#  created_at :datetime
#  updated_at :datetime
#

class User
	include Her::Model

	has_many :bookings

	@@ADMIN_GROUPS = [:digit, :prit, :styrit, :superadmin]
	@@FILTER = [:digit, :styrit, :prit, :nollkit, :sexit, :fanbarerit, :'8bit', :drawit, :armit, :hookit, :fritid, :snit, :flashit, :valberedningen, :laggit, :fikit, :dpo, :superadmin]

	# This MUST be fixed to user cid, but using cid outright causes infinite recursion
	def uid
		cid
	end

	def group_name
		self.relationships.map { | g | g["superGroup"]["name"].to_sym }
	end

	def symbol_groups
		@groups ||= (group_name(&:to_sym) & @@FILTER)
	end

	def admin?
		(symbol_groups & @@ADMIN_GROUPS).present?
	end

	def in_group?(group)
		symbol_groups.include? group
	end

	def user_profile_path
		"https://chalmers.it/author/#{cid}/"
	end

	def to_s
		"#{firstName} '#{nick}' #{lastName}"
	end

	alias_method :full_name, :to_s
end

class Symbol
	def itize
		case self
			when :digit, :styrit, :sexit, :fritid, :snit
				self.to_s.gsub /it/, 'IT'
			when :drawit, :armit, :hookit, :flashit, :laggit, :fikit
				self.to_s.titleize.gsub /it/, 'IT'
			when :'8bit'
				'8-bIT'
			when :nollkit
				'NollKIT'
			when :prit
				'P.R.I.T.'
			when :fanbarerit
				'FanbärerIT'
			when :valberedningen
				'Valberedningen'
			else
				self.to_s
		end
	end
end
